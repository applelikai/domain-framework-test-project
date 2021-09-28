using CloudEntity.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AutoIHome.Infrastructure.CloudEntity
{
    /// <summary>
    /// 实体分页列表
    /// </summary>
    /// <typeparam name="TModel">对象类型</typeparam>
    public class PagedList<TEntity> : IPagedList<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 分页查询的实体数据源
        /// </summary>
        private IDbPagedQuery<TEntity> _entities;
        /// <summary>
        /// 设置实体的匿名函数
        /// </summary>
        private Action<TEntity> _setEntity;
        /// <summary>
        /// 当前页
        /// </summary>
        private int _pageIndex;
        /// <summary>
        /// 每页元素数量
        /// </summary>
        private int _pageSize;
        /// <summary>
        /// 总数量
        /// </summary>
        private int _count;
        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageCount;

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
        }
        /// <summary>
        /// 每页元素数量
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
        }
        /// <summary>
        /// 总数量
        /// </summary>
        public int Count
        {
            get { return _count; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="entities">分页查询数据源</param>
        public PagedList(IDbPagedQuery<TEntity> entities)
        {
            //赋值
            _entities = entities;
            _pageIndex = entities.PageIndex;
            _pageSize = entities.PageSize;
            _count = entities.Count;
            _pageCount = entities.PageCount;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="entities">分页查询数据源</param>
        /// <param name="setEntity">设置实体的匿名函数</param>
        public PagedList(IDbPagedQuery<TEntity> entities, Action<TEntity> setEntity)
        {
            _entities = entities;
            _setEntity = setEntity;
            _pageIndex = entities.PageIndex;
            _pageSize = entities.PageSize;
            _count = entities.Count;
            _pageCount = entities.PageCount;
        }
        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            //创建遍历依次返回实体对象的等待机模型
            foreach (TEntity entity in _entities)
            {
                //设置实体信息
                if (_setEntity != null)
                    _setEntity(entity);
                //依次返回实体对象
                yield return entity;
            }
        }
        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
