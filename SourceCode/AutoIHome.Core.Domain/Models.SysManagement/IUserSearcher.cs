namespace AutoIHome.Core.Domain.Models.SysManagement
{
    /// <summary>
    /// 用户列表查询对象
    /// </summary>
    public interface IUserSearcher
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        string EmployeeName { get; }
    }
}
