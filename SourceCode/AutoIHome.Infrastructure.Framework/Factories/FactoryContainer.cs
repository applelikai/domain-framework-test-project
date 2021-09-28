using AutoIHome.Infrastructure.Framework.CloudEntity;
using CloudEntity.Data.Entity;
using Domain.Framework.Core.Factories;
using System;

namespace AutoIHome.Infrastructure.Framework.Factories
{
    /// <summary>
    /// 工厂容器类
    /// </summary>
    public sealed class FactoryContainer : FactoryContainerBase
    {
        /// <summary>
        /// 数据容器集对象
        /// </summary>
        private DbContainerSet _dbContainerSet;

        /// <summary>
        /// 创建仓库工厂对象
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>仓库工厂对象</returns>
        protected override IRepositoryFactory CreateRepositoryFactory(string assemblyName)
        {
            //获取数据容器
            IDbContainer container = _dbContainerSet.Get(assemblyName);
            //获取仓库工厂
            return new RepositoryFactory(container);
        }
        /// <summary>
        /// 创建数据库业务工厂对象
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>数据库业务工厂对象</returns>
        protected override IServiceFactory CreateServiceFactory(string assemblyName)
        {
            //获取业务工厂类型
            Type factoryType = Type.GetType(string.Format("{0}.CloudEntity.Framework.ServiceFactory, {0}.CloudEntity", assemblyName));
            //获取数据容器
            IDbContainer container = _dbContainerSet.Get(assemblyName);
            //获取业务工厂对象
            dynamic serviceFactory = Activator.CreateInstance(factoryType, container);
            return serviceFactory;
        }
        /// <summary>
        /// 创建第三方业务工厂对象
        /// </summary>
        /// <param name="extensionkillName">第三方技术名称</param>
        /// <returns>第三方业务工厂对象</returns>
        protected override IServiceFactory CreateExtensionServiceFactory(string extensionkillName)
        {
            //获取业务工厂类型
            string typeName = string.Format("Extension.{0}.Framework.ServiceFactory, Extension.{0}", extensionkillName);
            Type type = Type.GetType(typeName);
            if (type == null)
                return null;
            //获取业务工厂对象
            dynamic serviceFactory = Activator.CreateInstance(type);
            return serviceFactory;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dbContainerSet">数据容器集</param>
        public FactoryContainer(DbContainerSet dbContainerSet)
        {
            _dbContainerSet = dbContainerSet;
        }
        /// <summary>
        /// 获取第三方技术名称
        /// </summary>
        /// <param name="serviceBaseType">第三方业务基类类型</param>
        /// <returns>第三方技术名称</returns>
        public override string GetExtensionSkillName(Type serviceBaseType)
        {
            //获取程序集名称
            string assemblyName = serviceBaseType.Assembly.GetName().Name;
            //获取命名空间名称
            string namespaceName = serviceBaseType.Namespace;
            //获取自定义业务对象集对应的key
            int start = assemblyName.Length + 1;
            int end = namespaceName.IndexOf(".Services");
            if (start > end)
                return string.Empty;
            return namespaceName.Substring(start, end - start);
        }
    }
}
