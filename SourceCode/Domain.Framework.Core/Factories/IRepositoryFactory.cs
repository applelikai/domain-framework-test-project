using Domain.Framework.Core.Repositories;

namespace Domain.Framework.Core.Factories
{
    /// <summary>
    /// 创建仓库的工厂
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// 创建仓库对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓库对象</returns>
        IRepository<TEntity> Create<TEntity>()
            where TEntity : class;
    }
}
