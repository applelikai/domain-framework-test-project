using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Infrastructure.Models;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services.SysManagement
{
    /// <summary>
    /// 角色业务接口
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>结果及提示</returns>
        (bool, string) RemoveRole(string roleId);
        /// <summary>
        /// 保存角色权限菜单列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuNames">菜单名称数组</param>
        /// <returns>结果及提示</returns>
        (bool, string) SaveRoleMenes(string roleId, string[] menuNames);
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>角色分页列表</returns>
        IPagedList<Role> GetRoles(INameSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 角色业务扩展类
    /// </summary>
    public static class RoleServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IRoleService _Service
        {
            get { return ServiceContainer.Get<IRoleService>(); }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns>结果及提示</returns>
        public static (bool, string) Remove(this Role role)
        {
            return _Service.RemoveRole(role.RoleId);
        }
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>角色分页列表</returns>
        public static IPagedList<Role> GetRoles(this INameSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetRoles(searcher, pageIndex, pageSize);
        }
    }
}
