namespace AutoIHome.Core.Domain.Models.EmpManagement
{
    /// <summary>
    /// 职位列表查询对象
    /// </summary>
    public interface IJobSearcher
    {
        /// <summary>
        /// 所属部门id
        /// </summary>
        string DepartmentId { get; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        string DepartmentName { get; }
        /// <summary>
        /// 职位名称
        /// </summary>
        string JobName { get; }
    }
}
