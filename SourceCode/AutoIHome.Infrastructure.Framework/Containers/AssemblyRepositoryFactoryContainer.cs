using AutoIHome.Infrastructure.Framework.Factories;
using CloudEntity.Data.Entity;
using Domain.Framework.Core;
using Domain.Framework.Core.Factories;

namespace AutoIHome.Infrastructure.Framework.Containers
{
    /// <summary>
    /// 程序集数据仓库工厂容器类
    /// </summary>
    internal class AssemblyRepositoryFactoryContainer : ContainerBase<IRepositoryFactory>
    {
        /// <summary>
        /// 数据容器集对象
        /// </summary>
        private ContainerBase<IDbContainer> _dbContainerSet;

        /// <summary>
        /// 创建当前仓续集所在的仓库工厂
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>仓库工厂</returns>
        protected override IRepositoryFactory Create(string assemblyName)
        {
            //获取数据容器
            IDbContainer container = _dbContainerSet.Get(assemblyName);
            //获取仓库工厂
            return new RepositoryFactory(container);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dbContainerSet">数据容器集</param>
        public AssemblyRepositoryFactoryContainer(ContainerBase<IDbContainer> dbContainerSet)
        {
            _dbContainerSet = dbContainerSet;
        }
    }
}
