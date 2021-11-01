using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Core.Domain.Models.SysManagement;
using AutoIHome.Core.Domain.Services.SysManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Filters;
using AutoIHome.Platform.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace AutoIHome.Platform.Web.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>主页</returns>
        [ViewCheckLoginFilter()]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "主页";
            //返回视图
            return base.View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns>登录</returns>
        public IActionResult Login()
        {
            //返回主页面
            return base.View(new LoginUser());
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <returns>登录结果</returns>
        [HttpPost()]
        public IActionResult Login(LoginUser loginUser)
        {
            //初始化错误信息
            string errorMessage = string.Empty;
            //非空检查
            if (!loginUser.CheckNull(ref errorMessage))
            {
                base.ViewData["ErrorMessage"] = errorMessage;
                return base.View(loginUser);
            }
            //获取用户
            //User user = loginUser.GetUser();
            User user = new User()
            {
                UserId = GuidHelper.NewOrdered().ToString(),
                UserName = loginUser.UserName,
                Employee = new Employee(),
                Password = Md5Helper.GetMd5(loginUser.Password),
                RoleId = GuidHelper.NewOrdered().ToString()
            };
            if (user == null)
            {
                base.ViewData["ErrorMessage"] = $"用户({loginUser.UserName})不存在";
                return base.View(loginUser);
            }
            //检查密码
            if (!Md5Helper.GetMd5(loginUser.Password).Equals(user.Password))
            {
                base.ViewData["ErrorMessage"] = "密码错误";
                return base.View(loginUser);
            }
            //记录账户信息
            base.Response.Cookies.Append("UserId", user.UserId);
            base.Response.Cookies.Append("EmployeeNo", user.EmployeeNo ?? string.Empty);
            base.Response.Cookies.Append("UserName", HttpUtility.UrlEncode(user.Employee.EmployeeName ?? user.UserName));
            base.Response.Cookies.Append("RoleId", user.RoleId);
            //通过检查后跳转到主页
            return base.RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns>登录Action</returns>
        public IActionResult Logout()
        {
            //清空cookie
            base.Response.Cookies.Delete("UserId");
            base.Response.Cookies.Delete("EmployeeNo");
            base.Response.Cookies.Delete("UserName");
            base.Response.Cookies.Delete("RoleId");
            //跳转回登录
            return base.RedirectToAction("Login");
        }
    }
}
