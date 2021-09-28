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
        /// 工厂容器
        /// </summary>
        private static FactoryContainerBase _factoryContainer;
        /// <summary>
        /// 业务对象字典
        /// </summary>
        private static IDictionary<string, dynamic> _services;

        /// <summary>
        /// 获取创建业务实现对象的工厂
        /// </summary>
        /// <param name="serviceBaseType">业务基类类型</param>
        /// <returns>创建业务实现对象的工厂</returns>
        private static IServiceFactory GetFactory(Type serviceBaseType)
        {
            //获取第三方的技术名称
            string extensionSkillName = _factoryContainer.GetExtensionSkillName(serviceBaseType);
            //若当前业务类型指定为第三方技术的业务类型,则直接获取创建第三方业务对象的工厂
            if (!string.IsNullOrEmpty(extensionSkillName))
                return _factoryContainer.GetExtensionServiceFactory(extensionSkillName);
            //获取程序集名称
            string assemblyName = serviceBaseType.Assembly.GetName().Name;
            //获取当前程序集名称对应的业务对象创建工厂
            return _factoryContainer.GetServiceFactory(assemblyName);
        }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static ServiceContainer()
        {
            //初始化
            _locker = new object();
            _factoryContainer = ImplementContainer.Get<FactoryContainerBase>();
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
            //开始获取业务对象
            Start:
            //若字典中存在当前Service对象,直接获取
            if (_services.ContainsKey(serviceBaseType.FullName))
                return _services[serviceBaseType.FullName];
            //进入临界区(只允许一个线程进入)
            lock (_locker)
            {
                //若字典中不存在当前Service对象
                if (!_services.ContainsKey(serviceBaseType.FullName))
                {
                    //获取创建业务对象的工厂
                    IServiceFactory factory = ServiceContainer.GetFactory(serviceBaseType);
                    //创建并注册当前类型的业务对象
                    _services.Add(serviceBaseType.FullName, factory.Create<TService>());
                }
            }
            //回到Start
            goto Start;
        }
    }
}
