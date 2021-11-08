using System;

namespace Domain.Framework.Core.Factories
{
    /// <summary>
    /// 工厂获取接口
    /// </summary>
    public interface IFactoryGetter
    {
        /// <summary>
        /// 获取仓库工厂
        /// </summary>
        /// <param name="enttyType">实体类型</param>
        /// <returns>仓库工厂</returns>
        IRepositoryFactory GetRepositoryFactory(Type enttyType);
        /// <summary>
        /// 获取当前对象所在领域下的仓库工厂
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="objectId">对象id</param>
        /// <returns>仓库工厂</returns>
        IRepositoryFactory GetRepositoryFactory(Type entityType, string objectId);
        /// <summary>
        /// 获取业务工厂
        /// </summary>
        /// <param name="serviceBaseType">业务对象类型</param>
        /// <returns>业务工厂</returns>
        IServiceFactory GetServiceFactory(Type serviceBaseType);
        /// <summary>
        /// 获取当前对象所在领域下的业务工厂
        /// </summary>
        /// <param name="serviceBaseType">业务对象类型</param>
        /// <param name="objectId">对象id</param>
        /// <returns>业务工厂</returns>
        IServiceFactory GetServiceFactory(Type serviceBaseType, string objectId);
    }
}
