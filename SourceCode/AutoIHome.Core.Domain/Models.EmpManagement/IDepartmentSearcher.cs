namespace AutoIHome.Core.Domain.Models.EmpManagement
{
    /// <summary>
    /// 部门列表查询对象
    /// </summary>
    public interface IDepartmentSearcher
    {
        /// <summary>
        /// 上级部门id
        /// </summary>
        string ParentId { get; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        string ParentName { get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        string DepartmentName { get; }
    }
}
