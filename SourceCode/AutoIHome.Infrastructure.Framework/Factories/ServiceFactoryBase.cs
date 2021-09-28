using CloudEntity.Data.Entity;
using Domain.Framework.Core.Factories;
using System;

namespace AutoIHome.Infrastructure.Framework.Factories
{
    /// <summary>
    /// 创建业务实现对象的工厂基类
    /// </summary>
    public abstract class ServiceFactoryBase : IServiceFactory
    {
        /// <summary>
        /// 数据容器
        /// </summary>
        private IDbContainer _container;

        /// <summary>
        /// 获取实现类型
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>实现类型</returns>
        protected abstract Type GetImplementType(Type interfaceType);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ServiceFactoryBase(IDbContainer container)
        {
            _container = container;
        }
        /// <summary>
        /// 创建业务实现对象
        /// </summary>
        /// <typeparam name="TService">业务接口类型</typeparam>
        /// <returns>业务实现对象</returns>
        public TService Create<TService>()
        {
            //获取实现类型
            Type implementType = this.GetImplementType(typeof(TService));
            //创建实现类对象
            dynamic service = Activator.CreateInstance(implementType, _container);
            return service;
        }
    }
}
