using Domain.Framework.Core.Factories;
using Domain.Framework.Implementation;
using System;
using System.Collections.Generic;

namespace Domain.Framework.Core.Repositories
{
    /// <summary>
    /// 仓库容器类
    /// </summary>
    public static class RepositoryContainer
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _locker;
        /// <summary>
        /// 工厂获取对象
        /// </summary>
        private static IFactoryGetter _factoryGetter;
        /// <summary>
        /// 仓库字典
        /// </summary>
        private static IDictionary<string, dynamic> _repositories;

        /// <summary>
        /// 获取仓库对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="key">仓库对象对应的key</param>
        /// <param name="getFactory">获取仓库工厂的方法</param>
        /// <returns>仓库对象</returns>
        private static IRepository<TEntity> Get<TEntity>(string key, Func<IRepositoryFactory> getFactory)
            where TEntity : class
        {
            //开始
            Start:
            //若字典中包含当前仓库,则直接获取
            if (_repositories.ContainsKey(key))
                return _repositories[key];
            //进入临界区(只允许一个线程进入)
            lock (_locker)
            {
                //若repositories不包含当前类型的IRepository
                if (!_repositories.ContainsKey(key))
                {
                    //获取仓库工厂
                    IRepositoryFactory factory = getFactory();
                    //创建并注册仓库对象
                    _repositories.Add(key, factory.Create<TEntity>());
                }
            }
            //回到Start
            goto Start;
        }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static RepositoryContainer()
        {
            _locker = new object();
            _factoryGetter = ImplementContainer.Get<IFactoryGetter>();
            _repositories = new Dictionary<string, dynamic>();
        }
        /// <summary>
        /// 获取仓库对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓库</returns>
        public static IRepository<TEntity> Get<TEntity>()
            where TEntity : class
        {
            //获取实体类型
            Type entityType = typeof(TEntity);
            //获取仓库对象对应的key
            string key = entityType.FullName;
            //获取创建工厂的方法
            Func<IRepositoryFactory> getFactory = delegate ()
            {
                //获取当前程序集对应的仓库工厂
                return _factoryGetter.GetRepositoryFactory(entityType);
            };
            //获取仓库对象
            return RepositoryContainer.Get<TEntity>(key, getFactory);
        }
        /// <summary>
        /// 获取仓库对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="objectId">对象id</param>
        /// <returns>对象id所在领域的仓库对象</returns>
        public static IRepository<TEntity> Get<TEntity>(string objectId)
            where TEntity : class
        {
            //获取实体类型
            Type entityType = typeof(TEntity);
            //获取仓库对象对应的key
            string key = $"{objectId}:{entityType.FullName}";
            //获取创建工厂的方法
            Func<IRepositoryFactory> getFactory = delegate ()
            {
                //获取当前程序集对应的仓库工厂
                return _factoryGetter.GetRepositoryFactory(entityType, objectId);
            };
            //获取仓库对象
            return RepositoryContainer.Get<TEntity>(key, getFactory);
        }
    }
}
