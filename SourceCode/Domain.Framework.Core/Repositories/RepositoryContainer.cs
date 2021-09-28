using Domain.Framework.Core.Factories;
using Domain.Framework.Implementation;
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
        /// 工厂容器
        /// </summary>
        private static FactoryContainerBase _factoryContainer;
        /// <summary>
        /// 仓库字典
        /// </summary>
        private static IDictionary<string, dynamic> _repositories;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static RepositoryContainer()
        {
            _locker = new object();
            _factoryContainer = ImplementContainer.Get<FactoryContainerBase>();
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
            //获取实体类型名称
            string entityTypeName = typeof(TEntity).FullName;
            //开始
            Start:
            //若字典中包含当前仓库,则直接获取
            if (_repositories.ContainsKey(entityTypeName))
                return _repositories[entityTypeName];
            //进入临界区(只允许一个线程进入)
            lock (_locker)
            {
                //若repositories不包含当前类型的IRepository
                if (!_repositories.ContainsKey(entityTypeName))
                {
                    //获取当前实体类型所在程序集名称
                    string assemblyName = typeof(TEntity).Assembly.GetName().Name;
                    //获取当前程序集对应的仓库工厂
                    IRepositoryFactory factory = _factoryContainer.GetRepositoryFactory(assemblyName);
                    //创建并注册仓库对象
                    _repositories.Add(entityTypeName, factory.Create<TEntity>());
                }
            }
            //回到Start
            goto Start;
        }
    }
}
