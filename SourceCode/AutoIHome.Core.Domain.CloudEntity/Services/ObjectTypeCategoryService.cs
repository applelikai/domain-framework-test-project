using AutoIHome.Core.Domain.Entities;
using AutoIHome.Core.Domain.Models;
using AutoIHome.Core.Domain.Services;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using CloudEntity.Data.Entity;

namespace AutoIHome.Core.Domain.CloudEntity.Services
{
    /// <summary>
    /// 基础类型分类业务类
    /// </summary>
    internal class ObjectTypeCategoryService : BaseService, IObjectTypeCategoryService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ObjectTypeCategoryService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 分页获取基础类型分类列表
        /// </summary>
        /// <param name="searcher">基础类型分类列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>基础类型分类分页列表</returns>
        public IPagedList<ObjectTypeCategory> GetObjectTypeCategories(IObjectTypeCategorySearcher searcher, int pageIndex, int pageSize)
        {
            //获取基础类型分类数据源
            IDbQuery<ObjectTypeCategory> categories = base.Query<ObjectTypeCategory>();
            if (!string.IsNullOrEmpty(searcher.CategoryCode))
                categories = categories.Where(c => c.CategoryCode.Equals(searcher.CategoryCode));
            if (!string.IsNullOrEmpty(searcher.CategoryName))
                categories = categories.Like(c => c.CategoryName, $"%{searcher.CategoryName}%");
            //获取基础类型分类分页列表
            IDbPagedQuery<ObjectTypeCategory> pagedCategories = categories.PagingByDescending(c => c.SortedTime, pageSize, pageIndex);
            return new PagedList<ObjectTypeCategory>(pagedCategories);
        }
    }
}
