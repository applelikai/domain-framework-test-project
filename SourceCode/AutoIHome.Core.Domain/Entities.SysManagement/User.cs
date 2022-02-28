using AutoIHome.Core.Domain.Entities.EmpManagement;
using System;

namespace AutoIHome.Core.Domain.Entities.SysManagement
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public Employee Employee { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
    }
}
