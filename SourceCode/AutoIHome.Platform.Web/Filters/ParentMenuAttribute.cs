using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutoIHome.Platform.Web.Filters
{
    /// <summary>
    /// 父级菜单标识
    /// </summary>
    public class ParentMenuAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        private string _menuName;

        /// <summary>
        /// 菜单标识
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        public ParentMenuAttribute(string menuName)
        {
            _menuName = menuName;
        }
        /// <summary>
        /// 执行Action之前
        /// </summary>
        /// <param name="filterContext">Action上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取Controller
            Controller controller = filterContext.Controller as Controller;
            if (controller == null)
                return;
            //指定当前菜单
            controller.ViewData["ParentMenu"] = _menuName;
        }
    }
}
