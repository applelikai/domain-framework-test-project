using AutoIHome.Core.Domain.Entities;
using AutoIHome.Core.Domain.Services;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Areas.BaseManagement.Models;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Areas.BaseManagement.Controllers
{
    /// <summary>
    /// 基础类型分类控制器
    /// </summary>
    [Area("BaseManagement")]
    [Route("BaseManagement/[controller]/[action]")]
    public class ObjectTypeCategoryController : EntityController<ObjectTypeCategory>
    {
        /// <summary>
        /// 删除基础类型分类
        /// </summary>
        /// <param name="categoryCode">基础类型分类代码</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveObjectTypeCategory(string categoryCode)
        {
            //检查当前分类是否正被使用
            int count = RepositoryContainer.Get<ObjectType>().GetCount(t => t.CategoryCode.Equals(categoryCode));
            if (count > 0)
                return base.Message(false, "当前分类正被使用");
            //删除当前分类
            RepositoryContainer.Get<ObjectTypeCategory>().RemoveAll(c => c.CategoryCode.Equals(categoryCode));
            //获取结果提示
            return base.Message(true, "成功删除当前分类");
        }

        /// <summary>
        /// 基础类型分类
        /// </summary>
        /// <returns>基础类型分类</returns>
        [ViewCheckLoginFilter()]
        [Module("basic-data")]
        [Menu("edit-object-type-categories")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "基础类型分类";
            //获取视图参数
            base.ViewBag.LinksViewName = "_LinksEditObjectTypeCategory";
            //获取视图
            return base.View();
        }
        /// <summary>
        /// 基础类型管理
        /// </summary>
        /// <returns>基础类型管理</returns>
        [ViewCheckLoginFilter()]
        [Module("basic-data")]
        [Menu("edit-object-types")]
        public ViewResult ObjectTypes()
        {
            //获取标题
            base.ViewData["Title"] = "基础类型管理";
            //获取视图参数
            base.ViewBag.LinksViewName = "_LinkEditObjectTypes";
            //获取视图
            return base.View("Index");
        }

        /// <summary>
        /// 显示编辑基础类型分类
        /// </summary>
        /// <param name="categoryCode">基础类型分类代码</param>
        /// <returns>显示编辑基础类型分类的分部视图</returns>
        public PartialViewResult ShowEditObjectTypeCategory(string categoryCode)
        {
            //获取标题
            base.ViewData["Title"] = "编辑基础类型分类";
            //获取基础类型分类
            ObjectTypeCategory category = RepositoryContainer.Get<ObjectTypeCategory>().Get(categoryCode);
            //获取分部视图
            return base.PartialView("_EditObjectTypeCategory", category);
        }
        /// <summary>
        /// 显示基础类型分类详情
        /// </summary>
        /// <param name="categoryCode">基础类型分类代码</param>
        /// <returns>显示基础类型分类详情的分部视图</returns>
        public PartialViewResult ShowDetailObjectTypeCategory(string categoryCode)
        {
            //获取标题
            base.ViewData["Title"] = "基础类型分类详情";
            //获取基础类型分类
            ObjectTypeCategory category = RepositoryContainer.Get<ObjectTypeCategory>().Get(categoryCode);
            //获取分部视图
            return base.PartialView("_DetailObjectTypeCategory", category);
        }
        /// <summary>
        /// 分页显示基础类型分类列表
        /// </summary>
        /// <param name="searcher">基础类型分类列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示基础类型分类列表的分部视图</returns>
        public PartialViewResult ShowListPagedObjectTypeCategories(ObjectTypeCategorySearcher searcher, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //获取基础类型分类分页列表
            IPagedList<ObjectTypeCategory> categories = searcher.GetObjectTypeCategories(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedObjectTypeCategories", categories);
        }
    }
}
