using CloudEntity.Data.Entity;
using Domain.Framework.Core;
using Domain.Framework.Core.Factories;
using System;

namespace AutoIHome.Infrastructure.Framework.Containers
{
    /// <summary>
    /// 程序集业务工厂容器
    /// </summary>
    internal class AssemblyServiceFactoryContainer : ContainerBase<IServiceFactory>
    {
        /// <summary>
        /// 数据容器集对象
        /// </summary>
        private ContainerBase<IDbContainer> _dbContainerSet;

        /// <summary>
        /// 创建当前程序集所在的业务工厂
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>业务工厂</returns>
        protected override IServiceFactory Create(string assemblyName)
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
        /// 初始化
        /// </summary>
        /// <param name="dbContainerSet">数据容器集</param>
        public AssemblyServiceFactoryContainer(ContainerBase<IDbContainer> dbContainerSet)
        {
            _dbContainerSet = dbContainerSet;
        }
    }
}
