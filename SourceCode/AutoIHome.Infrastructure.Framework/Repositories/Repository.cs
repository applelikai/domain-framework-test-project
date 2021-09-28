using CloudEntity.Data.Entity;
using Domain.Framework.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoIHome.Infrastructure.Framework.Repositories
{
    /// <summary>
    /// 仓库类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    internal class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 数据容器
        /// </summary>
        private IDbContainer _container;

        /// <summary>
        /// 创建仓库对象
        /// </summary>
        /// <param name="container">数据容器</param>
        public Repository(IDbContainer container)
        {
            _container = container;
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        public int Add(TEntity entity)
        {
            return _container.List<TEntity>().Add(entity);
        }
        /// <summary>
        /// 保存实体信息至数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        public int Save(TEntity entity)
        {
            return _container.List<TEntity>().Save(entity);
        }
        /// <summary>
        /// 设置符合条件的实体对象的值
        /// </summary>
        /// <typeparam name="TModel">存储值的对象类型</typeparam>
        /// <param name="model">存储值的对象</param>
        /// <param name="predicate">实体对象筛选条件</param>
        /// <returns>受影响行数</returns>
        public int Set<TModel>(TModel model, Expression<Func<TEntity, bool>> predicate)
            where TModel : class
        {
            return _container.List<TEntity>().SetAll(model, predicate);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        public int Remove(TEntity entity)
        {
            return _container.List<TEntity>().Remove(entity);
        }
        /// <summary>
        /// 删除符合条件的实体对象
        /// </summary>
        /// <param name="predicate">实体对象筛选条件</param>
        /// <returns>受影响行数</returns>
        public int RemoveAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _container.List<TEntity>().RemoveAll(predicate);
        }
        /// <summary>
        /// 获取满足条件的实体数量
        /// </summary>
        /// <param name="predicate">对象筛选条件</param>
        /// <returns>满足条件的实体数量</returns>
        public int GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return _container.List<TEntity>().Count(predicate);
        }
        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>单个实体信息</returns>
        public TEntity Get(object id)
        {
            //若id为空,则返回空
            if (id == null)
                return null;
            //根据id获取实体对象
            return _container.List<TEntity>()[id];
        }
        /// <summary>
        /// 获取符合条件的单个实体对象
        /// </summary>
        /// <param name="predicate">实体对象查询表达式</param>
        /// <returns>单个实体对象</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _container.List<TEntity>().SingleOrDefault(predicate);
        }
        /// <summary>
        /// 获取所有的实体
        /// </summary>
        /// <returns>所有的实体</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _container.List<TEntity>();
        }
        /// <summary>
        /// 获取符合条件的实体对象列表
        /// </summary>
        /// <param name="predicate">实体对象筛选条件</param>
        /// <returns>符合条件的实体对象列表</returns>
        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return _container.List<TEntity>().Where(predicate);
        }
        /// <summary>
        /// 获取投影对象列表
        /// </summary>
        /// <typeparam name="TElement">投影类型</typeparam>
        /// <param name="selector">指定映射表达式</param>
        /// <returns>投影对象列表</returns>
        public IEnumerable<TElement> GetSelect<TElement>(Expression<Func<TEntity, TElement>> selector)
        {
            return _container.List<TEntity>().Select(selector);
        }
        /// <summary>
        /// 获取符合条件的投影对象列表
        /// </summary>
        /// <typeparam name="TElement">投影类型</typeparam>
        /// <param name="selector">指定映射表达式</param>
        /// <param name="predicate">对象筛选条件</param>
        /// <returns>投影对象列表</returns>
        public IEnumerable<TElement> GetSelect<TElement>(Expression<Func<TEntity, TElement>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            return _container.List<TEntity>().Where(predicate).Select(selector);
        }
    }
}
