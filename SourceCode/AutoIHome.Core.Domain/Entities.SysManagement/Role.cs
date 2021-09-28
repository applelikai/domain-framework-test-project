using System;

namespace AutoIHome.Core.Domain.Entities.SysManagement
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public Role() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="roleId">角色id</param>
        public Role(string roleId)
        {
            this.RoleId = roleId;
        }
    }
}
