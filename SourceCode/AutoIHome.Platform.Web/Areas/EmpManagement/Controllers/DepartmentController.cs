using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Services.EmpManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Areas.EmpManagement.Models;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoIHome.Platform.Web.Areas.EmpManagement.Controllers
{
    /// <summary>
    /// 部门控制器
    /// </summary>
    [Area("EmpManagement")]
    [Route("EmpManagement/[controller]/[action]")]
    public class DepartmentController : EntityController<Department>
    {
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveDepartment(string departmentId)
        {
            //检查当前部门是否被使用
            int count = RepositoryContainer.Get<Employee>().GetCount(e => e.DepartmentId.Equals(departmentId));
            if (count > 0)
                return base.Message(false, "当前部门已被使用");
            //删除部门
            RepositoryContainer.Get<Department>().RemoveAll(d => d.DepartmentId.Equals(departmentId));
            //获取结果提示
            return base.Message(true, "删除成功");
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns>部门列表</returns>
        public JsonResult GetDepartments()
        {
            //获取部门列表
            IEnumerable<Department> departments = RepositoryContainer.Get<Department>().GetAll();
            //获取部门列表json数据
            return base.Json(departments);
        }

        /// <summary>
        /// 部门管理
        /// </summary>
        /// <returns>部门管理</returns>
        [ViewCheckLoginFilter()]
        [Module("basic-management")]
        [ParentMenu("emp-management")]
        [Menu("edit-departments")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "部门管理";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示新建部门
        /// </summary>
        /// <param name="parentId">上级部门id</param>
        /// <returns>显示新建部门的分部视图</returns>
        public PartialViewResult ShowNewDepartment(string parentId)
        {
            //获取标题
            base.ViewData["Title"] = "新建部门";
            //获取上级部门
            Department parent = RepositoryContainer.Get<Department>().Get(parentId) ?? new Department();
            //获取分部视图
            return base.PartialView("_NewDepartment", parent);
        }
        /// <summary>
        /// 显示编辑部门
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <returns>显示编辑部门的分部视图</returns>
        public PartialViewResult ShowEditDepartment(string departmentId)
        {
            //获取标题
            base.ViewData["Title"] = "编辑部门";
            //获取部门
            Department department = RepositoryContainer.Get<Department>().Get(departmentId);
            department.Parent = RepositoryContainer.Get<DepartmentInfo>().Get(department.ParentId) ?? new DepartmentInfo();
            //获取分部视图
            return base.PartialView("_EditDepartment", department);
        }
        /// <summary>
        /// 显示部门详情
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <returns>显示部门详情的分部视图</returns>
        public PartialViewResult ShowDetailDepartment(string departmentId)
        {
            //获取标题
            base.ViewData["Title"] = "部门详情";
            //获取部门
            Department department = RepositoryContainer.Get<Department>().Get(departmentId);
            department.Parent = RepositoryContainer.Get<DepartmentInfo>().Get(department.ParentId) ?? new DepartmentInfo();
            //获取分部视图
            return base.PartialView("_DetailDepartment", department);
        }
        /// <summary>
        /// 分页显示部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示部门列表的分部视图</returns>
        public PartialViewResult ShowListPagedDepartments(DepartmentSearcher searcher, int pageIndex, int pageSize, string functionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            //获取部门分页列表
            IPagedList<Department> departments = searcher.GetDepartments(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedDepartments", departments);
        }
        /// <summary>
        /// 分页显示可选部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示可选部门列表的分部视图</returns>
        public PartialViewResult ShowListSelectDepartments(DepartmentSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取部门分页列表
            IPagedList<Department> departments = searcher.GetDepartments(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSelectDepartments", departments);
        }
        /// <summary>
        /// 分页显示查询可选部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数名称</param>
        /// <returns>分页显示查询可选部门列表的分部视图</returns>
        public PartialViewResult ShowListSearchDepartments(DepartmentSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取标题
            base.ViewData["Title"] = "可选部门列表";
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取部门分页列表
            IPagedList<Department> departments = (searcher ?? new DepartmentSearcher()).GetDepartments(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSearchDepartments", departments);
        }
    }
}
