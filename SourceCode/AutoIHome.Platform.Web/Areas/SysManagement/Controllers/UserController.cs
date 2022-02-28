using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Core.Domain.Services.SysManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Areas.SysManagement.Models;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Areas.SysManagement.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Area("SysManagement")]
    [Route("SysManagement/[controller]/[action]")]
    public class UserController : EntityController<User>
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果及提示</returns>
        public override JsonResult AddEntity(User entity)
        {
            //添加用户
            (bool status, string message) = entity.Register();
            //获取结果提示
            return base.Message(status, message);
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果及提示</returns>
        public override JsonResult SaveEntity(User entity)
        {
            //保存用户
            (bool status, string message) = entity.Save();
            //获取结果提示
            return base.Message(status, message);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        public JsonResult SavePassword(User user)
        {
            //修改密码
            var model = new { Password = Md5Helper.GetMd5(user.Password) };
            RepositoryContainer.Get<User>().Set(model, u => u.UserId.Equals(user.UserId));
            //获取结果提示
            return base.Message(true, "成功修改密码");
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveUser(string userId)
        {
            //删除用户
            RepositoryContainer.Get<User>().RemoveAll(u => u.UserId.Equals(userId));
            //获取结果提示
            return base.Message(true, "成功删除当前用户");
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns>用户管理</returns>
        [ViewCheckLoginFilter()]
        [Module("basic-management")]
        [ParentMenu("right-management")]
        [Menu("edit-users")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "用户管理";
            //获取视图参数
            base.ViewBag.LinksViewName = "_LinksEditUser";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>显示编辑用户的分部视图</returns>
        public PartialViewResult ShowEditUser(string userId)
        {
            //获取标题
            base.ViewData["Title"] = "编辑用户";
            //获取用户
            User user = RepositoryContainer.Get<User>().Get(userId);
            user.Role = RepositoryContainer.Get<Role>().Get(user.RoleId);
            user.Employee = RepositoryContainer.Get<Employee>().Get(user.EmployeeId) ?? new Employee();
            //获取分部视图
            return base.PartialView("_EditUser", user);
        }
        /// <summary>
        /// 显示修改当前登录用户密码
        /// </summary>
        /// <returns>显示修改当前登录用户密码的分部视图</returns>
        public PartialViewResult ShowEditPassword()
        {
            //获取标题
            base.ViewData["Title"] = "修改密码";
            //获取当前登录用户
            string userId = base.Request.Cookies["UserId"];
            User user = RepositoryContainer.Get<User>().Get(userId);
            //获取分部视图
            return base.PartialView("_EditPassword", user);
        }
        /// <summary>
        /// 显示用户详情
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>显示用户详情的分部视图</returns>
        public PartialViewResult ShowDetailUser(string userId)
        {
            //获取标题
            base.ViewData["Title"] = "用户详情";
            //获取用户
            User user = RepositoryContainer.Get<User>().Get(userId);
            user.Role = RepositoryContainer.Get<Role>().Get(user.RoleId);
            user.Employee = RepositoryContainer.Get<Employee>().Get(user.EmployeeId) ?? new Employee();
            //获取分部视图
            return base.PartialView("_DetailUser", user);
        }
        /// <summary>
        /// 分页显示用户列表
        /// </summary>
        /// <param name="searcher">用户列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示用户列表的分部视图</returns>
        public PartialViewResult ShowListPagedUsers(UserSearcher searcher, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //获取用户分页列表
            IPagedList<User> users = searcher.GetUsers(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedUsers", users);
        }
    }
}
