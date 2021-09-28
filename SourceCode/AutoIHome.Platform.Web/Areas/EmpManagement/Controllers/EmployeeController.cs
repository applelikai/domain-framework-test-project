using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Services.EmpManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Areas.EmpManagement.Models;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AutoIHome.Platform.Web.Areas.EmpManagement.Controllers
{
    /// <summary>
    /// 员工控制器
    /// </summary>
    [Area("EmpManagement")]
    [Route("EmpManagement/[controller]/[action]")]
    public class EmployeeController : EntityController<Employee>
    {
        /// <summary>
        /// web host环境对象
        /// </summary>
        private IWebHostEnvironment _env;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="env">web host环境对象</param>
        public EmployeeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 导出员工列表至excel
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <returns>excel文件结果</returns>
        public FileResult ExportEmployeesToExcel(EmployeeSearcher searcher)
        {
            //获取员工列表Excel文件
            string relativeFileName = searcher.ExportEmployees(_env.WebRootPath);
            //返回文件结果
            return base.File(relativeFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(relativeFileName));
        }

        /// <summary>
        /// 员工管理
        /// </summary>
        /// <returns>员工管理</returns>
        [ViewCheckLoginFilter()]
        [Module("emp-management")]
        [Menu("edit-employees")]
        public ViewResult Index()
        {
            //获取参数
            base.ViewData["Title"] = "员工管理";
            base.ViewData["IsDeleted"] = 0;
            base.ViewBag.LinksViewName = "_LinkEditEmployee";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑员工
        /// </summary>
        /// <param name="employeeNo">员工编号</param>
        /// <returns>显示编辑员工的分部视图</returns>
        public PartialViewResult ShowEditEmployee(string employeeNo)
        {
            //获取标题
            base.ViewData["Title"] = "编辑员工";
            //获取员工
            Employee employee = RepositoryContainer.Get<Employee>().Get(employeeNo);
            employee.Department = RepositoryContainer.Get<Department>().Get(employee.DepartmentNo) ?? new Department();
            employee.Job = RepositoryContainer.Get<Job>().Get(employee.JobId) ?? new Job();
            //获取分部视图
            return base.PartialView("_EditEmployee", employee);
        }
        /// <summary>
        /// 显示员工详情
        /// </summary>
        /// <param name="employeeNo">员工编号</param>
        /// <returns>显示员工详情的分部视图</returns>
        public PartialViewResult ShowDetailEmployee(string employeeNo)
        {
            //获取标题
            base.ViewData["Title"] = "员工详情";
            //获取员工
            Employee employee = RepositoryContainer.Get<Employee>().Get(employeeNo);
            employee.Department = RepositoryContainer.Get<Department>().Get(employee.DepartmentNo) ?? new Department();
            employee.Job = RepositoryContainer.Get<Job>().Get(employee.JobId) ?? new Job();
            //获取分部视图
            return base.PartialView("_DetailEmployee", employee);
        }
        /// <summary>
        /// 分页显示员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="linksViewName">操作元素按钮视图名称</param>
        /// <returns>分页显示员工列表的分部视图</returns>
        public PartialViewResult ShowListPagedEmployees(EmployeeSearcher searcher, int pageIndex, int pageSize, string functionName, string linksViewName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.LinksViewName = linksViewName;
            //获取员工分页列表
            IPagedList<Employee> employees = searcher.GetEmployees(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedEmployees", employees);
        }
        /// <summary>
        /// 分页显示可选员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示可选员工列表的分部视图</returns>
        public PartialViewResult ShowListSelectEmployees(EmployeeSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取员工分页列表
            IPagedList<Employee> employees = searcher.GetEmployees(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSelectEmployees", employees);
        }
        /// <summary>
        /// 分页显示查询可选员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示查询可选员工列表的分部视图</returns>
        public PartialViewResult ShowListSearchEmployees(EmployeeSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取标题
            base.ViewData["Title"] = "可选员工列表";
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取员工分页列表
            IPagedList<Employee> employees = searcher.GetEmployees(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSearchEmployees", employees);
        }
    }
}
