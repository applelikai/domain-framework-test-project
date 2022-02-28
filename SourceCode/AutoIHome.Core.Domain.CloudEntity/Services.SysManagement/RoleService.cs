using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Core.Domain.Services.SysManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using AutoIHome.Infrastructure.Models;
using CloudEntity.Data.Entity;

namespace AutoIHome.Core.Domain.CloudEntity.Services.SysManagement
{
    /// <summary>
    /// 角色业务类
    /// </summary>
    internal class RoleService : BaseService, IRoleService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public RoleService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>结果及提示</returns>
        public (bool, string) RemoveRole(string roleId)
        {
            //检查当前角色是否被使用
            if (base.Query<User>().Count(u => u.RoleId.Equals(roleId)) > 0)
                return (false, "当前角色正被使用");
            //开始事务处理
            using (IDbExecutor executor = base.Container.CreateExecutor())
            {
                //删除角色
                executor.Operator<Role>().RemoveAll(r => r.RoleId.Equals(roleId));
                //删除角色相关的权限
                executor.Operator<RoleMenu>().RemoveAll(m => m.RoleId.Equals(roleId));
                //提交修改
                executor.Commit();
                //获取结果及提示
                return (true, "删除成功");
            }
        }
        /// <summary>
        /// 保存角色权限菜单列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menus">菜单数组</param>
        /// <returns>结果及提示</returns>
        public (bool, string) SaveRoleMenes(string roleId, RoleMenu[] menus)
        {
            //开始事务处理
            using (IDbExecutor executor = base.Container.CreateExecutor())
            {
                //删除之前授权的菜单列表
                executor.Operator<RoleMenu>().RemoveAll(m => m.RoleId.Equals(roleId));
                //添加新授权的菜单列表
                foreach (RoleMenu menu in menus)
                {
                    menu.MenuId = GuidHelper.NewOrdered().ToString();
                    menu.RoleId = roleId;
                    executor.Operator<RoleMenu>().Add(menu);
                }
                //提交修改
                executor.Commit();
                //获取结果及提示
                return (true, "授权成功");
            }
        }
        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>角色分页列表</returns>
        public IPagedList<Role> GetRoles(INameSearcher searcher, int pageIndex, int pageSize)
        {
            //获取角色数据源
            IDbQuery<Role> roles = base.Query<Role>();
            if (!string.IsNullOrEmpty(searcher.SearchName))
                roles = roles.Like(r => r.RoleName, $"%{searcher.SearchName}%");
            //获取角色分页列表
            IDbPagedQuery<Role> pagedRoles = roles.PagingByDescending(r => r.CreatedTime, pageSize, pageIndex);
            return new PagedList<Role>(pagedRoles);
        }
    }
}
