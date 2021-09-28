using AutoIHome.Core.Domain.Models.SysManagement;

namespace AutoIHome.Platform.Web.Areas.SysManagement.Models
{
    /// <summary>
    /// 用户列表查询对象
    /// </summary>
    public class UserSearcher : IUserSearcher
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
    }
}
