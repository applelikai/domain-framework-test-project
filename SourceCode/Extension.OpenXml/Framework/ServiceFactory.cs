using Domain.Framework.Core.Factories;
using System;
using System.Reflection;

namespace Extension.OpenXml.Framework
{
    /// <summary>
    /// 创建业务实现对象的工厂类
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// 获取实现类型
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>实现类型</returns>
        private Type GetImplementType(Type interfaceType)
        {
            //获取当前程序集名称
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //获取领域名称(命名空间.Domain之前的字符串,如 AutoIHome.Core)
            string domainName = interfaceType.Namespace.Substring(0, interfaceType.Namespace.IndexOf(".Domain"));
            //获取模块名称(命名空间 Services及其之后的字符串,如 Services.EmpManagement)
            int start = interfaceType.Namespace.IndexOf("Services");
            string moduleName = interfaceType.Namespace.Substring(start, interfaceType.Namespace.Length - start);
            //获取实现类型的命名空间(如 Extension.OpenXml.AutoIHome.Core.Services.EmpManagement)
            string typeNamespace = $"{assemblyName}.{domainName}.{moduleName}";
            //获取实现类型全名
            string typeName = interfaceType.Name.Substring(1, interfaceType.Name.Length - 1);
            string typeFullName = $"{typeNamespace}.{typeName}";
            //获取实现类型
            return Type.GetType(typeFullName);
        }

        /// <summary>
        /// 创建业务实现对象
        /// </summary>
        /// <typeparam name="TService">业务接口类型</typeparam>
        /// <returns>业务实现对象</returns>
        public TService Create<TService>()
        {
            //获取实现类型
            Type implementType = this.GetImplementType(typeof(TService));
            //创建实现类对象
            dynamic service = Activator.CreateInstance(implementType);
            return service;
        }
    }
}
