using System;
using System.Collections.Generic;

namespace Domain.Framework.Core.Factories
{
    /// <summary>
    /// 工厂容器基类
    /// </summary>
    public abstract class FactoryContainerBase
    {
        /// <summary>
        /// 数据库仓库工厂字典线程锁对象
        /// </summary>
        private object _repositoryFactoriesLocker;
        /// <summary>
        /// 数据库业务工厂字典线程锁对象
        /// </summary>
        private object _serviceFactoriesLocker;
        /// <summary>
        /// 第三方业务工厂字典线程锁对象
        /// </summary>
        private object _extensionServiceFactoriesLocker;
        /// <summary>
        /// 数据库仓库工厂字典
        /// </summary>
        private IDictionary<string, IRepositoryFactory> _repositoryFactories;
        /// <summary>
        /// 数据库业务工厂字典
        /// </summary>
        private IDictionary<string, IServiceFactory> _serviceFactories;
        /// <summary>
        /// 第三方业务工厂字典
        /// </summary>
        private IDictionary<string, IServiceFactory> _extensionServiceFactories;

        /// <summary>
        /// 创建数据库仓库工厂对象
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>数据库仓库工厂对象</returns>
        protected abstract IRepositoryFactory CreateRepositoryFactory(string assemblyName);
        /// <summary>
        /// 创建数据库业务工厂对象
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>数据库业务工厂对象</returns>
        protected abstract IServiceFactory CreateServiceFactory(string assemblyName);
        /// <summary>
        /// 创建第三方业务工厂对象
        /// </summary>
        /// <param name="extensionkillName">第三方技术名称</param>
        /// <returns>第三方业务工厂对象</returns>
        protected abstract IServiceFactory CreateExtensionServiceFactory(string extensionkillName);

        /// <summary>
        /// 初始化
        /// </summary>
        public FactoryContainerBase()
        {
            //线程锁对象初始化
            _repositoryFactoriesLocker = new object();
            _serviceFactoriesLocker = new object();
            _extensionServiceFactoriesLocker = new object();
            //字典对象初始化
            _repositoryFactories = new Dictionary<string, IRepositoryFactory>();
            _serviceFactories = new Dictionary<string, IServiceFactory>();
            _extensionServiceFactories = new Dictionary<string, IServiceFactory>();
        }
        /// <summary>
        /// 获取第三方技术名称
        /// </summary>
        /// <param name="serviceBaseType">第三方业务基类类型</param>
        /// <returns>第三方技术名称</returns>
        public abstract string GetExtensionSkillName(Type serviceBaseType);
        /// <summary>
        /// 获取仓库工厂对象
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>仓库工厂对象</returns>
        public IRepositoryFactory GetRepositoryFactory(string assemblyName)
        {
            Start:
            //若字典中包含当前程序集名称对应的仓库工厂,则直接获取
            if (_repositoryFactories.ContainsKey(assemblyName))
                return _repositoryFactories[assemblyName];
            //进入临界区(只允许一个线程进入)
            lock (_repositoryFactoriesLocker)
            {
                //若字典中不包含当前程序集名称对应的仓库工厂
                if (!_repositoryFactories.ContainsKey(assemblyName))
                {
                    //则创建仓库工厂并添加
                    _repositoryFactories.Add(assemblyName, this.CreateRepositoryFactory(assemblyName));
                }
            }
            //回到Start
            goto Start;
        }
        /// <summary>
        /// 获取业务工厂对象
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>业务工厂对象</returns>
        public IServiceFactory GetServiceFactory(string assemblyName)
        {
            Start:
            //若字典中包含当前程序集名称对应的业务工厂,则直接获取
            if (_serviceFactories.ContainsKey(assemblyName))
                return _serviceFactories[assemblyName];
            //进入临界区(只允许一个线程进入)
            lock (_serviceFactoriesLocker)
            {
                //若字典中不包含当前程序集名称对应的业务工厂
                if (!_serviceFactories.ContainsKey(assemblyName))
                {
                    //则创建业务工厂并添加
                    _serviceFactories.Add(assemblyName, this.CreateServiceFactory(assemblyName));
                }
            }
            //回到Start
            goto Start;
        }
        /// <summary>
        /// 获取第三方业务工厂对象
        /// </summary>
        /// <param name="extensionkillName">第三方技术名称</param>
        /// <returns>第三方业务工厂对象</returns>
        public IServiceFactory GetExtensionServiceFactory(string extensionkillName)
        {
            Start:
            //若字典中包含当前第三方技术名称对应的业务工厂,则直接获取
            if (_extensionServiceFactories.ContainsKey(extensionkillName))
                return _extensionServiceFactories[extensionkillName];
            //进入临界区(只允许一个线程进入)
            lock (_extensionServiceFactoriesLocker)
            {
                //若字典中不包含当第三方技术集名称对应的业务工厂
                if (!_extensionServiceFactories.ContainsKey(extensionkillName))
                {
                    //则创建业务工厂并添加
                    _extensionServiceFactories.Add(extensionkillName, this.CreateExtensionServiceFactory(extensionkillName));
                }
            }
            //回到Start
            goto Start;
        }
    }
}
