using CloudEntity.Data;
using CloudEntity.Data.Entity;
using System;
using System.Linq.Expressions;

namespace AutoIHome.Infrastructure.Framework.Services
{
    /// <summary>
    /// 业务基类
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// 数据容器
        /// </summary>
        protected IDbContainer Container { get; private set; }
        /// <summary>
        /// 操作数据库的DbHelper类
        /// </summary>
        protected DbHelper DbHelper
        {
            get { return this.Container.DbHelper; }
        }

        /// <summary>
        /// 是否存在满足条件的实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="predicate">存在条件</param>
        /// <returns>true:存在 false:不存在</returns>
        protected bool Exist<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return this.Container.List<TEntity>().Count(predicate) > 0;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>数据列表</returns>
        protected IDbList<TEntity> List<TEntity>()
            where TEntity : class
        {
            return this.Container.List<TEntity>();
        }
        /// <summary>
        /// 查询获取实体数据源
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>实体数据源</returns>
        protected IDbQuery<TEntity> Query<TEntity>()
            where TEntity : class
        {
            return this.Container.List<TEntity>();
        }
        /// <summary>
        /// 查询获取实体数据源
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="predicate">查询条件表达式</param>
        /// <returns>实体数据源</returns>
        protected IDbQuery<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return this.Container.List<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 创建业务对象
        /// </summary>
        /// <param name="container">数据容器</param>
        public BaseService(IDbContainer container)
        {
            this.Container = container;
        }
    }
}
