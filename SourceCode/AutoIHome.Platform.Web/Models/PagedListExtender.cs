using AutoIHome.Infrastructure;

namespace AutoIHome.Platform.Web.Models
{
    /// <summary>
    /// 分页列表扩展类
    /// </summary>
    public static class PagedListExtender
    {
        /// <summary>
        /// 获取分页对象
        /// </summary>
        /// <typeparam name="TModel">元素类型</typeparam>
        /// <param name="source">元素分页列表</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页对象</returns>
        public static Pagination GetPagination<TModel>(this IPagedList<TModel> source, string functionName)
            where TModel : class
        {
            //获取分页对象
            return new Pagination(functionName)
            {
                PageIndex = source.PageIndex,
                PageCount = source.PageCount,
                PageSize = source.PageSize,
                Count = source.Count
            };
        }
    }
}
