using AutoIHome.Infrastructure;
using AutoIHome.Platform.Web.Areas.RegManagement.Models;
using AutoIHome.Platform.Web.Controllers;
using AutoIHome.Platform.Web.Filters;
using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.Services;
using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AutoIHome.Platform.Web.Areas.RegManagement.Controllers
{
    /// <summary>
    /// 县级行政区控制器
    /// </summary>
    [Area("RegManagement")]
    [Route("RegManagement/[controller]/[action]")]
    public class CountyLevelController : EntityController<CountyLevel>
    {
        /// <summary>
        /// web host环境对象
        /// </summary>
        private IWebHostEnvironment _env;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="env">web host环境对象</param>
        public CountyLevelController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 删除县级行政区
        /// </summary>
        /// <param name="countyCode">县级行政区代码</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveCountyLevel(string countyCode)
        {
            //删除县级行政区
            RepositoryContainer.Get<CountyLevel>().RemoveAll(c => c.CountyCode.Equals(countyCode));
            //获取结果提示
            return base.Message(true, "删除成功");
        }
        /// <summary>
        /// 导出县级行政区列表至Excel文件
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <returns>excel文件结果</returns>
        public FileResult ExportCountyLevelsToExcel(CountyLevelSearcher searcher)
        {
            //获取县级行政区列表Excel文件
            string relativeFileName = searcher.ExportCountyLevels(_env.WebRootPath);
            //获取文件结果
            return base.File(relativeFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(relativeFileName));
        }

        /// <summary>
        /// 县级行政区
        /// </summary>
        [ViewCheckLoginFilter()]
        [Module("reg-management")]
        [Menu("edit-county-levels")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "县级行政区";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑县级行政区
        /// </summary>
        /// <param name="countyCode">县级行政区代码</param>
        /// <returns>显示编辑县级行政区的分部视图</returns>
        public PartialViewResult ShowEditCountyLevel(string countyCode)
        {
            //获取标题
            base.ViewData["Title"] = "编辑县级行政区";
            //获取县级行政区
            CountyLevel countyLevel = RepositoryContainer.Get<CountyLevel>().Get(countyCode);
            countyLevel.ProvinceLevel = RepositoryContainer.Get<ProvinceLevel>().Get(countyLevel.ProvinceCode);
            countyLevel.PrefectureLevel = RepositoryContainer.Get<PrefectureLevel>().Get(countyLevel.PrefectureCode) ?? new PrefectureLevel();
            //获取分部视图
            return base.PartialView("_EditCountyLevel", countyLevel);
        }
        /// <summary>
        /// 显示县级行政区详情
        /// </summary>
        /// <param name="countyCode">县级行政区代码</param>
        /// <returns>显示县级行政区详情的分部视图</returns>
        public PartialViewResult ShowDetailCountyLevel(string countyCode)
        {
            //获取标题
            base.ViewData["Title"] = "县级行政区详情";
            //获取县级行政区
            CountyLevel countyLevel = RepositoryContainer.Get<CountyLevel>().Get(countyCode);
            countyLevel.ProvinceLevel = RepositoryContainer.Get<ProvinceLevel>().Get(countyLevel.ProvinceCode);
            countyLevel.PrefectureLevel = RepositoryContainer.Get<PrefectureLevel>().Get(countyLevel.PrefectureCode) ?? new PrefectureLevel();
            //获取分部视图
            return base.PartialView("_DetailCountyLevel", countyLevel);
        }
        /// <summary>
        /// 分页显示县级行政区列表
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示县级行政区列表的分部视图</returns>
        public PartialViewResult ShowListPagedCountyLevels(CountyLevelSearcher searcher, int pageIndex, int pageSize, string functionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            //获取县级行政区分页列表
            IPagedList<CountyLevel> countyLevels = searcher.GetCountyLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedCountyLevels", countyLevels);
        }
    }
}
