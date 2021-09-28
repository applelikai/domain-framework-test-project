using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Core.Domain.Services.CfgManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using AutoIHome.Infrastructure.Models;
using CloudEntity.Data.Entity;

namespace AutoIHome.Core.Domain.CloudEntity.Services.CfgManagement
{
    /// <summary>
    /// 参数分类业务类
    /// </summary>
    internal class ParameterCategoryService : BaseService, IParameterCategoryService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ParameterCategoryService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 分页获取参数分类列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分类分页列表</returns>
        public IPagedList<ParameterCategory> GetParameterCategories(INameSearcher searcher, int pageIndex, int pageSize)
        {
            //获取参数分类数据源
            IDbQuery<ParameterCategory> categories = base.Query<ParameterCategory>();
            if (!string.IsNullOrEmpty(searcher.SearchName))
                categories = categories.Like(c => c.CategoryName, $"%{searcher.SearchName}%");
            //获取参数分类分页列表
            IDbPagedQuery<ParameterCategory> pagedCategories = categories.PagingByDescending(c => c.SortedTime, pageSize, pageIndex);
            return new PagedList<ParameterCategory>(pagedCategories);
        }
    }
}
