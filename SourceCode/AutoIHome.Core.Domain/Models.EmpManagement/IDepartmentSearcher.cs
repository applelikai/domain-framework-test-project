namespace AutoIHome.Core.Domain.Models.EmpManagement
{
    /// <summary>
    /// 部门列表查询对象
    /// </summary>
    public interface IDepartmentSearcher
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        string DepartmentNo { get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        string DepartmentName { get; }
    }
}
