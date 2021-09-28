using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Services.EmpManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using AutoIHome.Platform.Web.Models;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Areas.EmpManagement.Controllers
{
    /// <summary>
    /// 职位控制器
    /// </summary>
    [Area("EmpManagement")]
    [Route("EmpManagement/[controller]/[action]")]
    public class JobController : EntityController<Job>
    {
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="jobId">职位id</param>
        /// <returns>结果提示</returns>
        public JsonResult RemoveJob(string jobId)
        {
            //检查当前职位是否被使用
            int count = RepositoryContainer.Get<Employee>().GetCount(e => e.JobId.Equals(jobId));
            if (count > 0)
                return base.Message(false, "当前职位已被使用");
            //删除职位
            RepositoryContainer.Get<Job>().RemoveAll(j => j.JobId.Equals(jobId));
            //获取结果提示
            return base.Message(true, "删除成功");
        }

        /// <summary>
        /// 职位管理
        /// </summary>
        /// <returns>职位管理</returns>
        [ViewCheckLoginFilter()]
        [Module("emp-management")]
        [Menu("edit-jobs")]
        public ViewResult Index()
        {
            //获取参数
            base.ViewData["Title"] = "职位管理";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑职位
        /// </summary>
        /// <param name="jobId">职位id</param>
        /// <returns>显示编辑职位的分部视图</returns>
        public PartialViewResult ShowEditJob(string jobId)
        {
            //获取标题
            base.ViewData["Title"] = "编辑职位";
            //获取职位
            Job job = RepositoryContainer.Get<Job>().Get(jobId);
            //获取分部视图
            return base.PartialView("_EditJob", job);
        }
        /// <summary>
        /// 分页显示职位列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示职位列表的分部视图</returns>
        public PartialViewResult ShowListPagedJobs(NameSearcher searcher, int pageIndex, int pageSize, string functionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            //获取职位分页列表
            IPagedList<Job> jobs = searcher.GetJobs(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedJobs", jobs);
        }
        /// <summary>
        /// 分页显示可选职位列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数名称</param>
        /// <returns>分页显示可选职位列表的分部视图</returns>
        public PartialViewResult ShowListSelectJobs(NameSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取职位分页列表
            IPagedList<Job> jobs = searcher.GetJobs(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSelectJobs", jobs);
        }
        /// <summary>
        /// 分页显示查询可选职位列表
        /// </summary>
        /// <param name="searcher">名称项列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数名称</param>
        /// <returns>分页显示查询可选职位列表的分部视图</returns>
        public PartialViewResult ShowListSearchJobs(NameSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取标题
            base.ViewData["Title"] = "可选职位列表";
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取职位分页列表
            IPagedList<Job> jobs = (searcher ?? new NameSearcher()).GetJobs(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSearchJobs", jobs);
        }
    }
}
