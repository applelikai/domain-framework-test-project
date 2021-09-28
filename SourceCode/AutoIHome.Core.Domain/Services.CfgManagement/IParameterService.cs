using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Core.Domain.Models.CfgManagement;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services.CfgManagement
{
    /// <summary>
    /// 参数业务接口
    /// </summary>
    public interface IParameterService
    {
        /// <summary>
        /// 分页获取参数列表
        /// </summary>
        /// <param name="searcher">参数列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分页列表</returns>
        IPagedList<Parameter> GetParameters(IParameterSearcher searcher, int pageIndex, int pageSize);
        /// <summary>
        /// 分页获取当前分类下的参数列表
        /// </summary>
        /// <param name="category">参数分类</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分页列表</returns>
        IPagedList<Parameter> GetParameters(ParameterCategory category, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 参数业务扩展类
    /// </summary>
    public static class ParameterServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IParameterService _Service
        {
            get { return ServiceContainer.Get<IParameterService>(); }
        }

        /// <summary>
        /// 分页获取参数列表
        /// </summary>
        /// <param name="searcher">参数列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分页列表</returns>
        public static IPagedList<Parameter> GetParameters(this IParameterSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetParameters(searcher, pageIndex, pageSize);
        }
        /// <summary>
        /// 分页获取当前分类下的参数列表
        /// </summary>
        /// <param name="category">参数分类</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分页列表</returns>
        public static IPagedList<Parameter> GetParameters(this ParameterCategory category, int pageIndex, int pageSize)
        {
            return _Service.GetParameters(category, pageIndex, pageSize);
        }
    }
}
