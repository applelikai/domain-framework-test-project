namespace AutoIHome.Core.Domain.Models
{
    /// <summary>
    /// 基础类型分类查询对象
    /// </summary>
    public interface IObjectTypeCategorySearcher
    {
        /// <summary>
        /// 分类代码
        /// </summary>
        string CategoryCode { get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        string CategoryName { get; }
    }
}
