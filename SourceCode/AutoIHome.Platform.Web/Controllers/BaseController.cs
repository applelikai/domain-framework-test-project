using AutoIHome.Platform.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 返回结果提示信息
        /// </summary>
        /// <param name="status">执行成功或失败</param>
        /// <param name="message">提示信息</param>
        /// <returns>结果提示</returns>
        protected JsonResult Message(bool status, string message)
        {
            return base.Json(new { status, message });
        }

        /// <summary>
        /// 显示局部视图
        /// </summary>
        /// <param name="viewName">分部视图名称</param>
        /// <param name="title">标题</param>
        /// <returns>局部视图的html字符串</returns>
        public PartialViewResult ShowView(string viewName, string title)
        {
            //获取参数
            base.ViewBag.Title = title;
            //返回分部视图
            return base.PartialView(viewName);
        }
        /// <summary>
        /// 显示分页信息
        /// </summary>
        /// <param name="pagination">分页对象</param>
        /// <returns>显示分页信息的局部视图</returns>
        public PartialViewResult ShowPaging(Pagination pagination)
        {
            //获取分部视图
            return base.PartialView("_Pagination", pagination);
        }
    }
}
