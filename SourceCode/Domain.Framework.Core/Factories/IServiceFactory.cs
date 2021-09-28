namespace Domain.Framework.Core.Factories
{
    /// <summary>
    /// 创建业务实现对象的工厂
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// 创建业务实现对象
        /// </summary>
        /// <typeparam name="TService">业务接口类型</typeparam>
        /// <returns>业务实现对象</returns>
        TService Create<TService>();
    }
}
