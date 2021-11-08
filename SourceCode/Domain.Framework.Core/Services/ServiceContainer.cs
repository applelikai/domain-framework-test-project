using Domain.Framework.Core.Factories;
using Domain.Framework.Implementation;
using System;
using System.Collections.Generic;

namespace Domain.Framework.Core.Services
{
    /// <summary>
    /// 业务对象容器类
    /// </summary>
    public static class ServiceContainer
    {
        /// <summary>
        /// 线程锁对象
        /// </summary>
        private static object _locker;
        /// <summary>
        /// 工厂获取对象
        /// </summary>
        private static IFactoryGetter _factoryGetter;
        /// <summary>
        /// 业务对象字典
        /// </summary>
        private static IDictionary<string, dynamic> _services;

        /// <summary>
        /// 获取业务对象
        /// </summary>
        /// <typeparam name="TService">业务对象类型</typeparam>
        /// <param name="key">业务对象对应的key</param>
        /// <param name="getFactory">创建业务对象工厂的函数</param>
        /// <returns>业务对象</returns>
        private static TService Get<TService>(string key, Func<IServiceFactory> getFactory)
        {
            //开始获取业务对象
            Start:
            //若字典中存在当前Service对象,直接获取
            if (_services.ContainsKey(key))
                return _services[key];
            //进入临界区(只允许一个线程进入)
            lock (_locker)
            {
                //若字典中不存在当前Service对象
                if (!_services.ContainsKey(key))
                {
                    //获取创建业务对象的工厂
                    IServiceFactory factory = getFactory();
                    //创建并注册当前类型的业务对象
                    _services.Add(key, factory.Create<TService>());
                }
            }
            //回到Start
            goto Start;
        }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static ServiceContainer()
        {
            //初始化
            _locker = new object();
            _factoryGetter = ImplementContainer.Get<IFactoryGetter>();
            _services = new Dictionary<string, dynamic>();
        }
        /// <summary>
        /// 获取业务处理对象
        /// </summary>
        /// <typeparam name="TService">业务对象类型</typeparam>
        /// <returns>业务处理对象</returns>
        public static TService Get<TService>()
        {
            //获取业务基类类型
            Type serviceBaseType = typeof(TService);
            //获取创建业务对象工厂的函数
            Func<IServiceFactory> getFactory = delegate ()
            {
                //获取创建业务对象的工厂
                return _factoryGetter.GetServiceFactory(serviceBaseType);
            };
            //获取业务对象
            return ServiceContainer.Get<TService>(serviceBaseType.FullName, getFactory);
        }
        /// <summary>
        /// 获取当前对象id所在领域下的业务处理对象
        /// </summary>
        /// <typeparam name="TService">业务对象类型</typeparam>
        /// <param name="objectId">对象id</param>
        /// <returns>业务处理对象</returns>
        public static TService Get<TService>(string objectId)
        {
            //获取业务基类类型
            Type serviceBaseType = typeof(TService);
            //获取业务对象对应的key
            string key = $"{objectId}:{serviceBaseType.FullName}";
            //获取创建业务对象工厂的函数
            Func<IServiceFactory> getFactory = delegate ()
            {
                //获取创建业务对象的工厂
                return _factoryGetter.GetServiceFactory(serviceBaseType, objectId);
            };
            //获取业务对象
            return ServiceContainer.Get<TService>(key, getFactory);
        }
    }
}
