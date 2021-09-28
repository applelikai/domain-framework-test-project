using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Core.Domain.Services.SysManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using AutoIHome.Platform.Web.Models;
using Domain.Framework.Core.Repositories;
using Domain.Framework.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoIHome.Platform.Web.Areas.SysManagement.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Area("SysManagement")]
    [Route("SysManagement/[controller]/[action]")]
    public class RoleController : EntityController<Role>
    {
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveRole(string roleId)
        {
            //获取当前角色
            Role role = new Role(roleId);
            //删除角色
            (bool status, string message) = role.Remove();
            //获取结果及提示
            return base.Message(status, message);
        }
        /// <summary>
        /// 为角色授权菜单列表访问权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuNames">菜单名称数组</param>
        /// <returns>结果及提示</returns>
        public JsonResult SetRoleMenus(string roleId, string[] menuNames)
        {
            //非空检查
            if (string.IsNullOrEmpty(roleId))
                return base.Message(false, "请先设置要授权的角色");
            if (menuNames == null)
                return base.Message(false, "请先勾选要授权的菜单");
            if (menuNames.Length == 0)
                return base.Message(false, "请先勾选要授权的菜单");
            //保存角色菜单列表
            (bool status, string message) = ServiceContainer.Get<IRoleService>().SaveRoleMenes(roleId, menuNames);
            //获取结果及提示
            return base.Message(status, message);
        }
        /// <summary>
        /// 获取当前角色授权的菜单列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>菜单名称列表</returns>
        public JsonResult GetRoleMenus(string roleId)
        {
            //查询当前角色授权的菜单列表
            IEnumerable<string> menuNames = RepositoryContainer.Get<RoleMenu>()
                .GetSelect(m => m.MenuName, m => m.RoleId.Equals(roleId));
            //获取当前角色授权的菜单列表
            return base.Json(menuNames);
        }
        /// <summary>
        /// 获取当前登录的角色授权的菜单列表
        /// </summary>
        /// <returns>菜单名称列表</returns>
        public JsonResult GetLoginRoleMenus()
        {
            //获取当前登录的角色id
            string roleId = base.Request.Cookies["RoleId"];
            //查询当前角色授权的菜单列表
            IEnumerable<string> menuNames = RepositoryContainer.Get<RoleMenu>()
                .GetSelect(m => m.MenuName, m => m.RoleId.Equals(roleId));
            //获取当前角色授权的菜单列表
            return base.Json(menuNames);
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns>角色管理</returns>
        [ViewCheckLoginFilter()]
        [Module("setting")]
        [Menu("edit-roles")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "角色管理";
            //获取视图参数
            base.ViewBag.LinksViewName = "_LinksEditRole";
            //获取视图
            return base.View();
        }
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns>角色授权</returns>
        [ViewCheckLoginFilter()]
        [Module("setting")]
        [Menu("edit-role-rights")]
        public ViewResult Right()
        {
            //获取参数
            base.ViewData["Title"] = "角色授权";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>显示编辑角色的分部视图</returns>
        public PartialViewResult ShowEditRole(string roleId)
        {
            //获取标题
            base.ViewData["Title"] = "编辑角色";
            //获取角色
            Role role = RepositoryContainer.Get<Role>().Get(roleId);
            //获取分部视图
            return base.PartialView("_EditRole", role);
        }
        /// <summary>
        /// 分页显示角色列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示角色列表的分部视图</returns>
        public PartialViewResult ShowListPagedRoles(NameSearcher searcher, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //获取角色分页列表
            IPagedList<Role> roles = searcher.GetRoles(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedRoles", roles);
        }
        /// <summary>
        /// 分页显示可选角色列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数名称</param>
        /// <returns>分页显示可选角色列表的分部视图</returns>
        public PartialViewResult ShowListSelectRoles(NameSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取角色分页列表
            IPagedList<Role> roles = searcher.GetRoles(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSelectRoles", roles);
        }
        /// <summary>
        /// 分页显示查询可选角色列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数名称</param>
        /// <returns>分页显示查询可选角色列表的分部视图</returns>
        public PartialViewResult ShowListSearchRoles(NameSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取标题
            base.ViewData["Title"] = "可选角色列表";
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取角色分页列表
            IPagedList<Role> roles = searcher.GetRoles(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSearchRoles", roles);
        }
    }
}
