using AutoIHome.Core.Domain.Entities;
using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Core.Domain.Services.CfgManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using AutoIHome.Platform.Web.Models;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Areas.CfgManagement.Controllers
{
    /// <summary>
    /// 参数分类控制器
    /// </summary>
    [Area("CfgManagement")]
    [Route("CfgManagement/[controller]/[action]")]
    public class ParameterCategoryController : BaseController
    {
        /// <summary>
        /// 删除参数分类
        /// </summary>
        /// <param name="categoryId">参数分类id</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveParameterCategory(string categoryId)
        {
            //检查当前参数分类下是否有参数
            int count = RepositoryContainer.Get<Parameter>().GetCount(p => p.CategoryId.Equals(categoryId));
            if (count > 0)
                return base.Message(false, "当前参数分类正被使用");
            //删除参数分类
            RepositoryContainer.Get<ObjectType>().RemoveAll(t => t.TypeId.Equals(categoryId));
            //获取结果及提示
            return base.Message(true, "成功删除当前参数分类");
        }

        /// <summary>
        /// 参数分类
        /// </summary>
        /// <returns>参数分类</returns>
        [ViewCheckLoginFilter()]
        [Module("setting")]
        [Menu("edit-parameter-categories")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "参数分类";
            //获取视图参数
            base.ViewBag.LinksViewName = "_LinkRemoveParameterCategory";
            //获取视图
            return base.View();
        }
        /// <summary>
        /// 配置参数
        /// </summary>
        /// <returns>配置参数</returns>
        [ViewCheckLoginFilter()]
        [Module("setting")]
        [Menu("edit-parameters")]
        public ViewResult Parameters()
        {
            //获取标题
            base.ViewData["Title"] = "配置参数";
            //获取视图参数
            base.ViewBag.LinksViewName = "_LinkEditParameters";
            //获取视图
            return base.View("Index");
        }

        /// <summary>
        /// 分页显示参数分类列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示参数分类列表的分部视图</returns>
        public PartialViewResult ShowListPagedParameterCategories(NameSearcher searcher, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //虎丘参数分类分页列表
            IPagedList<ParameterCategory> categories = searcher.GetParameterCategories(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedParameterCategories", categories);
        }
    }
}
