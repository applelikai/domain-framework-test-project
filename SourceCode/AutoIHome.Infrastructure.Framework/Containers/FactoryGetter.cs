using AutoIHome.Infrastructure.Framework.CloudEntity;
using CloudEntity.Data.Entity;
using Domain.Framework.Core;
using Domain.Framework.Core.Factories;
using Microsoft.Extensions.Configuration;
using System;

namespace AutoIHome.Infrastructure.Framework.Containers
{
    /// <summary>
    /// 工厂获取类
    /// </summary>
    public class FactoryGetter : IFactoryGetter
    {
        /// <summary>
        /// 程序集数据仓库工厂容器
        /// </summary>
        private ContainerBase<IRepositoryFactory> _assemblyRepositoryFactoryContainer;
        /// <summary>
        /// 对象id数据仓库工厂容器
        /// </summary>
        private ContainerBase<IRepositoryFactory> _objectIdRepositoryFactoryContainer;
        /// <summary>
        /// 程序集业务工厂容器
        /// </summary>
        private ContainerBase<IServiceFactory> _assemblyServiceFactoryContainer;
        /// <summary>
        /// 对象id业务工厂容器
        /// </summary>
        private ContainerBase<IServiceFactory> _objectIdServiceFactoryContainer;
        /// <summary>
        /// 第三方业务工厂容器
        /// </summary>
        private ContainerBase<IServiceFactory> _extensionServiceFactoryContainer;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">配置</param>
        public FactoryGetter(IConfiguration configuration)
        {
            //获取数据容器集对象
            ContainerBase<IDbContainer> dbContainerSet = new DbContainerSet(configuration);
            //初始化工厂容器
            _assemblyRepositoryFactoryContainer = new AssemblyRepositoryFactoryContainer(dbContainerSet);
            _assemblyServiceFactoryContainer = new AssemblyServiceFactoryContainer(dbContainerSet);
            _extensionServiceFactoryContainer = new ExtensionServiceFactoryContainer();
        }
        /// <summary>
        /// 获取仓库工厂
        /// </summary>
        /// <param name="enttyType">实体类型</param>
        /// <returns>仓库工厂</returns>
        public IRepositoryFactory GetRepositoryFactory(Type enttyType)
        {
            //获取当前实体类型所在程序集名称
            string assemblyName = enttyType.Assembly.GetName().Name;
            //获取当前程序集对应的仓库工厂
            return _assemblyRepositoryFactoryContainer.Get(assemblyName);
        }
        /// <summary>
        /// 获取当前对象所在领域下的仓库工厂
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="objectId">对象id</param>
        /// <returns>仓库工厂</returns>
        public IRepositoryFactory GetRepositoryFactory(Type entityType, string objectId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取业务工厂
        /// </summary>
        /// <param name="serviceBaseType">业务对象类型</param>
        /// <returns>业务工厂</returns>
        public IServiceFactory GetServiceFactory(Type serviceBaseType)
        {
            //获取程序集名称
            string assemblyName = serviceBaseType.Assembly.GetName().Name;
            //获取命名空间名称
            string namespaceName = serviceBaseType.Namespace;
            //获取自定义业务对象工厂
            int start = assemblyName.Length + 1;
            int end = namespaceName.IndexOf(".Services");
            if (end > start)
            {
                string name = namespaceName.Substring(start, end - start);
                return _extensionServiceFactoryContainer.Get(name);
            }
            //获取当前程序集对应的业务对象工厂
            return _assemblyServiceFactoryContainer.Get(assemblyName); ;
        }
        /// <summary>
        /// 获取当前对象所在领域下的业务工厂
        /// </summary>
        /// <param name="serviceBaseType">业务对象类型</param>
        /// <param name="objectId">对象id</param>
        /// <returns>业务工厂</returns>
        public IServiceFactory GetServiceFactory(Type serviceBaseType, string objectId)
        {
            throw new NotImplementedException();
        }
    }
}
