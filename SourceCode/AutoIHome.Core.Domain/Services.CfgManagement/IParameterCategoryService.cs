using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Infrastructure.Models;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services.CfgManagement
{
    /// <summary>
    /// 参数分类业务接口
    /// </summary>
    public interface IParameterCategoryService
    {
        /// <summary>
        /// 分页获取参数分类列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分类分页列表</returns>
        IPagedList<ParameterCategory> GetParameterCategories(INameSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 参数分类业务扩展类
    /// </summary>
    public static class ParameterCategoryServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IParameterCategoryService _Service
        {
            get { return ServiceContainer.Get<IParameterCategoryService>(); }
        }

        /// <summary>
        /// 分页获取参数分类列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分类分页列表</returns>
        public static IPagedList<ParameterCategory> GetParameterCategories(this INameSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetParameterCategories(searcher, pageIndex, pageSize);
        }
    }
}
