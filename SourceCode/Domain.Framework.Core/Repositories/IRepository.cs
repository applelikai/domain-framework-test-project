using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Framework.Core.Repositories
{
    /// <summary>
    /// 仓库接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        int Add(TEntity entity);
        /// <summary>
        /// 保存实体信息至数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        int Save(TEntity entity);
        /// <summary>
        /// 设置符合条件的实体对象的值
        /// </summary>
        /// <typeparam name="TModel">存储值的对象类型</typeparam>
        /// <param name="model">存储值的对象</param>
        /// <param name="predicate">实体对象筛选条件</param>
        /// <returns>受影响行数</returns>
        int Set<TModel>(TModel model, Expression<Func<TEntity, bool>> predicate)
            where TModel : class;
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        int Remove(TEntity entity);
        /// <summary>
        /// 删除符合条件的实体对象
        /// </summary>
        /// <param name="predicate">实体对象筛选条件</param>
        /// <returns>受影响行数</returns>
        int RemoveAll(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取满足条件的实体数量
        /// </summary>
        /// <param name="predicate">对象筛选条件</param>
        /// <returns>满足条件的实体数量</returns>
        int GetCount(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取单个实体信息
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>单个实体信息</returns>
        TEntity Get(object id);
        /// <summary>
        /// 获取符合条件的单个实体对象
        /// </summary>
        /// <param name="predicate">实体对象查询表达式</param>
        /// <returns>单个实体对象</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取所有的实体
        /// </summary>
        /// <returns>所有的实体</returns>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// 获取符合条件的实体对象列表
        /// </summary>
        /// <param name="predicate">实体对象筛选条件</param>
        /// <returns>符合条件的实体对象列表</returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取投影对象列表
        /// </summary>
        /// <typeparam name="TElement">投影类型</typeparam>
        /// <param name="selector">指定映射表达式</param>
        /// <returns>投影对象列表</returns>
        IEnumerable<TElement> GetSelect<TElement>(Expression<Func<TEntity, TElement>> selector);
        /// <summary>
        /// 获取符合条件的投影对象列表
        /// </summary>
        /// <typeparam name="TElement">投影类型</typeparam>
        /// <param name="selector">指定映射表达式</param>
        /// <param name="predicate">对象筛选条件</param>
        /// <returns>投影对象列表</returns>
        IEnumerable<TElement> GetSelect<TElement>(Expression<Func<TEntity, TElement>> selector, Expression<Func<TEntity, bool>> predicate);
    }
}
