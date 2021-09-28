using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Core.Domain.Models.CfgManagement;
using AutoIHome.Core.Domain.Services.CfgManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using CloudEntity.Data.Entity;

namespace AutoIHome.Core.Domain.CloudEntity.Services.CfgManagement
{
    /// <summary>
    /// 参数业务类
    /// </summary>
    internal class ParameterService : BaseService, IParameterService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ParameterService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 分页获取参数列表
        /// </summary>
        /// <param name="searcher">参数列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分页列表</returns>
        public IPagedList<Parameter> GetParameters(IParameterSearcher searcher, int pageIndex, int pageSize)
        {
            //获取参数分类数据源
            IDbQuery<ParameterCategory> categories = base.Query<ParameterCategory>()
                .IncludeBy(c => c.CategoryName);
            if (!string.IsNullOrEmpty(searcher.CategoryName))
                categories = categories.Like(c => c.CategoryName, $"%{searcher.CategoryName}%");
            //获取参数数据源
            IDbQuery<Parameter> parameters = base.Query<Parameter>()
                .Join(categories, p => p.Category, (p, c) => p.CategoryId == c.CategoryId);
            if (!string.IsNullOrEmpty(searcher.ParameterName))
                parameters = parameters.Like(p => p.ParameterName, $"%{searcher.ParameterName}%");
            //获取参数分页列表
            IDbPagedQuery<Parameter> pagedParameters = parameters
                .OrderByDescending(p => p.Category.SortedTime)
                .ThenByDescending(p => p.SortedTime)
                .Paging(pageSize, pageIndex);
            return new PagedList<Parameter>(pagedParameters);
        }
        /// <summary>
        /// 分页获取当前分类下的参数列表
        /// </summary>
        /// <param name="category">参数分类</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>参数分页列表</returns>
        public IPagedList<Parameter> GetParameters(ParameterCategory category, int pageIndex, int pageSize)
        {
            //获取参数数据源
            IDbQuery<Parameter> parameters = base.Query<Parameter>(p => p.CategoryId.Equals(category.CategoryId));
            //获取参数分页列表
            IDbPagedQuery<Parameter> pagedParameters = parameters.PagingByDescending(p => p.SortedTime, pageSize, pageIndex);
            return new PagedList<Parameter>(pagedParameters, p => p.Category = category);
        }
    }
}
