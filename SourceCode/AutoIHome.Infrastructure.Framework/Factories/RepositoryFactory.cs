using AutoIHome.Infrastructure.Framework.Repositories;
using CloudEntity.Data.Entity;
using Domain.Framework.Core.Factories;
using Domain.Framework.Core.Repositories;

namespace AutoIHome.Infrastructure.Framework.Factories
{
    /// <summary>
    /// 创建仓库的工厂
    /// </summary>
    internal class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// 数据容器
        /// </summary>
        private IDbContainer _container;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public RepositoryFactory(IDbContainer container)
        {
            _container = container;
        }
        /// <summary>
        /// 创建仓库对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓库对象</returns>
        public IRepository<TEntity> Create<TEntity>()
            where TEntity : class
        {
            return new Repository<TEntity>(_container);
        }
    }
}
