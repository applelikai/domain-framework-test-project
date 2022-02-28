using AutoIHome.Core.Domain.Models.EmpManagement;

namespace AutoIHome.Platform.Web.Areas.EmpManagement.Models
{
    /// <summary>
    /// 部门列表查询对象
    /// </summary>
    public class DepartmentSearcher : IDepartmentSearcher
    {
        /// <summary>
        /// 上级部门id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
