using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutoIHome.Platform.Web.Filters
{
    /// <summary>
    /// 模块标识
    /// </summary>
    public class ModuleAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        private string moduleName;

        /// <summary>
        /// 模块标识
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        public ModuleAttribute(string moduleName)
        {
            this.moduleName = moduleName;
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
            //指定当前模块
            controller.ViewData["Module"] = this.moduleName;
        }
    }
}
