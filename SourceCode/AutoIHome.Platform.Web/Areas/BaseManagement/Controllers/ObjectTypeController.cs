using AutoIHome.Core.Domain.Entities;
using AutoIHome.Core.Domain.Services;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Controllers;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Areas.BaseManagement.Controllers
{
    /// <summary>
    /// 基础类型控制器
    /// </summary>
    [Area("BaseManagement")]
    [Route("BaseManagement/[controller]/[action]")]
    public class ObjectTypeController : EntityController<ObjectType>
    {
        /// <summary>
        /// 显示新建基础类型
        /// </summary>
        /// <param name="categoryCode">基础类型分类代码</param>
        /// <returns>显示新建基础类型的分部视图</returns>
        public PartialViewResult ShowNewObjectType(string categoryCode)
        {
            //获取标题
            base.ViewData["Title"] = "新建基础类型";
            //获取类型分类
            ObjectTypeCategory category = RepositoryContainer.Get<ObjectTypeCategory>().Get(categoryCode);
            //获取分部视图
            return base.PartialView("_NewObjectType", category);
        }
        /// <summary>
        /// 显示编辑基础类型
        /// </summary>
        /// <param name="typeId">基础类型id</param>
        /// <returns>显示编辑基础类型的分部视图</returns>
        public PartialViewResult ShowEditObjectType(string typeId)
        {
            //获取标题
            base.ViewData["Title"] = "编辑基础类型";
            //获取基础类型
            ObjectType objectType = RepositoryContainer.Get<ObjectType>().Get(typeId);
            objectType.Category = RepositoryContainer.Get<ObjectTypeCategory>().Get(objectType.CategoryCode);
            //获取分部视图
            return base.PartialView("_EditObjectType", objectType);
        }
        /// <summary>
        /// 显示基础类型详情
        /// </summary>
        /// <param name="typeId">基础类型id</param>
        /// <returns>显示基础类型详情的分部视图</returns>
        public PartialViewResult ShowDetailObjectType(string typeId)
        {
            //获取标题
            base.ViewData["Title"] = "基础类型详情";
            //获取基础类型
            ObjectType objectType = RepositoryContainer.Get<ObjectType>().Get(typeId);
            objectType.Category = RepositoryContainer.Get<ObjectTypeCategory>().Get(objectType.CategoryCode);
            //获取分部视图
            return base.PartialView("_DetailObjectType", objectType);
        }
        /// <summary>
        /// 分页显示基础类型列表
        /// </summary>
        /// <param name="categoryCode">分类代码</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示基础类型列表的分部视图</returns>
        public PartialViewResult ShowListPagedObjectTypes(string categoryCode, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //获取基础类型分页列表
            ObjectTypeCategory category = RepositoryContainer.Get<ObjectTypeCategory>().Get(categoryCode);
            IPagedList<ObjectType> objectTypes = category.GetObjectTypes(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedObjectTypes", objectTypes);
        }
        /// <summary>
        /// 分页显示编辑基础类型列表
        /// </summary>
        /// <param name="categoryCode">分类代码</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示编辑基础类型列表的分部视图</returns>
        public PartialViewResult ShowListEditObjectTypes(string categoryCode, int pageIndex, int pageSize, string functionName)
        {
            //获取标题
            base.ViewData["Title"] = "编辑基础类型列表";
            //获取分类代码
            base.ViewData["CategoryCode"] = categoryCode;
            //获取视图参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = "_LinkEditObjectType";
            //获取基础类型分页列表
            ObjectTypeCategory category = RepositoryContainer.Get<ObjectTypeCategory>().Get(categoryCode);
            IPagedList<ObjectType> objectTypes = category.GetObjectTypes(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListEditObjectTypes", objectTypes);
        }
    }
}
