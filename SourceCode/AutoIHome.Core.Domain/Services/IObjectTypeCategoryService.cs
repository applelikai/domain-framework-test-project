using AutoIHome.Core.Domain.Entities;
using AutoIHome.Core.Domain.Models;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services
{
    /// <summary>
    /// 基础类型分类业务接口
    /// </summary>
    public interface IObjectTypeCategoryService
    {
        /// <summary>
        /// 分页获取基础类型分类列表
        /// </summary>
        /// <param name="searcher">基础类型分类列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>基础类型分类分页列表</returns>
        IPagedList<ObjectTypeCategory> GetObjectTypeCategories(IObjectTypeCategorySearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 基础类型分类业务扩展类
    /// </summary>
    public static class ObjectTypeCategoryServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IObjectTypeCategoryService _Service
        {
            get { return ServiceContainer.Get<IObjectTypeCategoryService>(); }
        }

        /// <summary>
        /// 分页获取基础类型分类列表
        /// </summary>
        /// <param name="searcher">基础类型分类列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>基础类型分类分页列表</returns>
        public static IPagedList<ObjectTypeCategory> GetObjectTypeCategories(this IObjectTypeCategorySearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetObjectTypeCategories(searcher, pageIndex, pageSize);
        }
    }
}
