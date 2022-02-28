using AutoIHome.Core.Domain.Models.EmpManagement;

namespace AutoIHome.Platform.Web.Areas.EmpManagement.Models
{
    /// <summary>
    /// 职位列表查询对象
    /// </summary>
    public class JobSearcher : IJobSearcher
    {
        /// <summary>
        /// 所属部门id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
    }
}
