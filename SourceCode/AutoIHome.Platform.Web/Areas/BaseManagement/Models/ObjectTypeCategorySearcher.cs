using AutoIHome.Core.Domain.Models;

namespace AutoIHome.Platform.Web.Areas.BaseManagement.Models
{
    /// <summary>
    /// 基础类型分类查询对象
    /// </summary>
    public class ObjectTypeCategorySearcher : IObjectTypeCategorySearcher
    {
        /// <summary>
        /// 分类代码
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
    }
}
