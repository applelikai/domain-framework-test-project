using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Domain.Framework.Implementation
{
    /// <summary>
    /// 实现对象容器
    /// </summary>
    public static class ImplementContainer
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _locker;
        /// <summary>
        /// 实现对象字典
        /// </summary>
        private static IDictionary<string, dynamic> _implementObjects;

        /// <summary>
        /// 初始化
        /// </summary>
        static ImplementContainer()
        {
            _locker = new object();
            _implementObjects = new Dictionary<string, dynamic>();
        }
        /// <summary>
        /// 获取实现类型
        /// </summary>
        /// <param name="interfaceType">接口类型名称</param>
        /// <returns>实现类型</returns>
        private static Type GetImplementType(Type interfaceType)
        {
            //初始化类型名称
            StringBuilder typeNameBuilder = new StringBuilder();
            //拼接类型全名
            ImplementTypeAttribute typeAttribute = interfaceType.GetCustomAttribute<ImplementTypeAttribute>();
            if (typeAttribute != null)
                typeNameBuilder.Append(typeAttribute.TypeFullName);
            //拼接程序集名称
            ImplementAssemblyAttribute assemblyAttribute = interfaceType.GetCustomAttribute<ImplementAssemblyAttribute>();
            if (assemblyAttribute != null)
                typeNameBuilder.AppendFormat(", {0}", assemblyAttribute.AssemblyName);
            //获取类型全名
            string typeName = typeNameBuilder.ToString();
            if (typeName.Length == 0)
                return null;
            //获取实现类型
            return Type.GetType(typeNameBuilder.ToString());
        }

        /// <summary>
        /// 添加实现对象
        /// </summary>
        /// <typeparam name="TParent">父类型</typeparam>
        /// <param name="implementation">实现对象</param>
        public static void Add<TParent>(TParent implementation)
        {
            //获取父类型全名
            string parentTypeName = typeof(TParent).FullName;
            //若字典中存在当前父类型实现对象,直接赋值
            if (_implementObjects.ContainsKey(parentTypeName))
                _implementObjects[parentTypeName] = implementation;
            //若不存在则直接添加
            else
                _implementObjects.Add(parentTypeName, implementation);
        }
        /// <summary>
        /// 获取接口实现对象
        /// </summary>
        /// <typeparam name="TParent">父类型</typeparam>
        /// <returns>实现对象</returns>
        public static TParent Get<TParent>()
        {
            //获取父类型
            Type parentType = typeof(TParent);
            //获取实现对象
            Start:
            //若字典中存在当前父类型实现对象,直接获取
            if (_implementObjects.ContainsKey(parentType.FullName))
                return _implementObjects[parentType.FullName];
            //进入临界区
            lock (_locker)
            {
                //若字典中不存在当前Service对象,则创建并添加至字典
                if (!_implementObjects.ContainsKey(parentType.FullName))
                {
                    //获取实现类型
                    Type implementType = ImplementContainer.GetImplementType(parentType);
                    //实现类型不为空则添加
                    if (implementType != null)
                        _implementObjects.Add(parentType.FullName, Activator.CreateInstance(implementType));
                    //实现类型为空，则直接获取父类型默认值
                    else
                        return default(TParent);
                }
                //回到Start
                goto Start;
            }
        }
    }
}
