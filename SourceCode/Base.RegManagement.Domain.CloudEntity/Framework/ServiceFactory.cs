using AutoIHome.Infrastructure.Framework.Factories;
using CloudEntity.Data.Entity;
using System;
using System.Reflection;

namespace Base.RegManagement.Domain.CloudEntity.Framework
{
    /// <summary>
    /// 创建业务实现对象的工厂类
    /// </summary>
    public class ServiceFactory : ServiceFactoryBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ServiceFactory(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取实现类型
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>实现类型</returns>
        protected override Type GetImplementType(Type interfaceType)
        {
            //获取当前程序集名称
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //获取实现类型的全名
            string typeNamespace = interfaceType.Namespace.Replace(interfaceType.Assembly.GetName().Name, assemblyName);
            string typeName = interfaceType.Name.Substring(1, interfaceType.Name.Length - 1);
            string typeFullName = string.Format("{0}.{1}", typeNamespace, typeName);
            //获取实现类型
            return Type.GetType(typeFullName);
        }
    }
}
