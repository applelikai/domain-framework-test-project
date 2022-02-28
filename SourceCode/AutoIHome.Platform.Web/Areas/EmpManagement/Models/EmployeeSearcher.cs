using AutoIHome.Core.Domain.Models.EmpManagement;

namespace AutoIHome.Platform.Web.Areas.EmpManagement.Models
{
    /// <summary>
    /// 员工列表查询对象
    /// </summary>
    public class EmployeeSearcher : IEmployeeSearcher
    {
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 是否删除(0:未删除 1:已删除)
        /// </summary>
        public int? IsDeleted { get; set; }
    }
}
