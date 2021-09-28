namespace AutoIHome.Core.Domain.Models.EmpManagement
{
    /// <summary>
    /// 员工列表查询对象
    /// </summary>
    public interface IEmployeeSearcher
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        string EmployeeNo { get; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        string EmployeeName { get; }
        /// <summary>
        /// 部门
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// 职位
        /// </summary>
        string JobName { get; }
        /// <summary>
        /// 是否删除(0:未删除 1:已删除)
        /// </summary>
        int? IsDeleted { get; }
    }
}
