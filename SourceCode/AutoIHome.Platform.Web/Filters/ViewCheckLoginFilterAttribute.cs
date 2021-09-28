using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Web;

namespace AutoIHome.Platform.Web.Filters
{
    /// <summary>
    /// 登录检查拦截器
    /// </summary>
    public class ViewCheckLoginFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行Action之前拦截
        /// </summary>
        /// <param name="context">上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //若已登陆,则退出
            if (context.HttpContext.Request.Cookies.ContainsKey("UserName"))
            {
                //获取UserName
                string userName = HttpUtility.UrlDecode(context.HttpContext.Request.Cookies["UserName"]);
                //记录UserName
                (context.Controller as Controller).ViewData["UserName"] = userName;
                //退出
                return;
            }
            //回到登录界面
            RouteValueDictionary routeValues = new RouteValueDictionary(new
            {
                Action = "Login",
                Controller = "Home",
                Area = ""
            });
            context.Result = new RedirectToRouteResult(routeValues);
        }
    }
}
