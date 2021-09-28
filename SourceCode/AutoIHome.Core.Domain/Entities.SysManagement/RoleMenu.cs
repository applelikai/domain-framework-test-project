using System;

namespace AutoIHome.Core.Domain.Entities.SysManagement
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    public class RoleMenu
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 授权时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public RoleMenu() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="menuId">菜单id</param>
        public RoleMenu(string menuId)
        {
            this.MenuId = menuId;
        }
    }
}
