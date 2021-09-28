using System.Collections.Generic;

namespace AutoIHome.Infrastructure
{
    /// <summary>
    /// 分页列表
    /// </summary>
    /// <typeparam name="TModel">对象类型</typeparam>
    public interface IPagedList<out TModel> : IEnumerable<TModel>
        where TModel : class
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 每页元素数量
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 总数量
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }
    }
}
