using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Core.Domain.Services.CfgManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Areas.CfgManagement.Models;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Areas.CfgManagement.Controllers
{
    /// <summary>
    /// 参数控制器
    /// </summary>
    [Area("CfgManagement")]
    [Route("CfgManagement/[controller]/[action]")]
    public class ParameterController : EntityController<Parameter>
    {
        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveParameter(string parameterName)
        {
            //删除参数
            RepositoryContainer.Get<Parameter>().RemoveAll(p => p.ParameterName.Equals(parameterName));
            //获取结果及提示
            return base.Message(true, "成功删除当前参数");
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <returns>查询参数</returns>
        [ViewCheckLoginFilter()]
        [Module("setting")]
        [Menu("search-parameters")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "查询参数";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示新建参数
        /// </summary>
        /// <param name="categoryId">参数分类id</param>
        /// <returns>显示新建参数的分部视图</returns>
        public PartialViewResult ShowNewParameter(string categoryId)
        {
            //获取标题
            base.ViewData["Title"] = "新建参数";
            //获取参数分类
            ParameterCategory category = RepositoryContainer.Get<ParameterCategory>().Get(categoryId);
            //获取分部视图
            return base.PartialView("_NewParameter", category);
        }
        /// <summary>
        /// 显示编辑参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns>显示编辑参数的分部视图</returns>
        public PartialViewResult ShowEditParameter(string parameterName)
        {
            //获取标题
            base.ViewData["Title"] = "编辑参数";
            //获取参数
            Parameter parameter = RepositoryContainer.Get<Parameter>().Get(parameterName);
            parameter.Category = RepositoryContainer.Get<ParameterCategory>().Get(parameter.CategoryId);
            //获取分部视图
            return base.PartialView("_EditParameter", parameter);
        }
        /// <summary>
        /// 显示参数详情
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns>显示参数详情的分部视图</returns>
        public PartialViewResult ShowDetailParameter(string parameterName)
        {
            //获取标题
            base.ViewData["Title"] = "参数详情";
            //获取参数
            Parameter parameter = RepositoryContainer.Get<Parameter>().Get(parameterName);
            parameter.Category = RepositoryContainer.Get<ParameterCategory>().Get(parameter.CategoryId);
            //获取分部视图
            return base.PartialView("_DetailParameter", parameter);
        }
        /// <summary>
        /// 分页显示参数列表
        /// </summary>
        /// <param name="searcher">参数列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示参数列表的分部视图</returns>
        public PartialViewResult ShowListPagedParameters(ParameterSearcher searcher, int pageIndex, int pageSize, string functionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            //获取参数分页列表
            IPagedList<Parameter> parameters = searcher.GetParameters(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedParameters", parameters);
        }
        /// <summary>
        /// 分页显示参数列表
        /// </summary>
        /// <param name="categoryId">参数分类id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示参数列表的分部视图</returns>
        public PartialViewResult ShowListPagedParametersByCategory(string categoryId, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //获取参数分页列表
            ParameterCategory category = RepositoryContainer.Get<ParameterCategory>().Get(categoryId);
            IPagedList<Parameter> parameters = category.GetParameters(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedParameters", parameters);
        }
        /// <summary>
        /// 分页显示编辑参数列表
        /// </summary>
        /// <param name="categoryId">参数分类id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示编辑参数列表的分部视图</returns>
        public PartialViewResult ShowListEditParameters(string categoryId, int pageIndex, int pageSize, string functionName)
        {
            //获取标题
            base.ViewData["Title"] = "编辑参数列表";
            //获取分类id
            base.ViewData["CategoryId"] = categoryId;
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = "_LinksEditParameter";
            //获取参数分页列表
            ParameterCategory category = RepositoryContainer.Get<ParameterCategory>().Get(categoryId);
            IPagedList<Parameter> parameters = category.GetParameters(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListEditParameters", parameters);
        }
    }
}
