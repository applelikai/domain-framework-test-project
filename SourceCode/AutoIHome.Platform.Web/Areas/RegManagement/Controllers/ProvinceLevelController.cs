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
    /// 省级行政区控制器
    /// </summary>
    [Area("RegManagement")]
    [Route("RegManagement/[controller]/[action]")]
    public class ProvinceLevelController : EntityController<ProvinceLevel>
    {
        /// <summary>
        /// web host环境对象
        /// </summary>
        private IWebHostEnvironment _env;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="env">web host环境对象</param>
        public ProvinceLevelController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 删除省级行政区
        /// </summary>
        /// <param name="provinceCode">省份代码</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemoveProvinceLevel(string provinceCode)
        {
            //检查当前省级行政区下有没有地级行政区
            int prefectureLevelCount = RepositoryContainer.Get<PrefectureLevel>().GetCount(p => p.ProvinceCode.Equals(provinceCode));
            if (prefectureLevelCount > 0)
                return base.Message(false, "当前省级行政区下有地级行政区，不可删除");
            //检查当前省级行政区下有没有县级行政区
            int countyLevelCount = RepositoryContainer.Get<CountyLevel>().GetCount(c => c.ProvinceCode.Equals(provinceCode));
            if (countyLevelCount > 0)
                return base.Message(false, "当前省级行政区下有县级行政区，不可删除");
            //删除省级行政区
            RepositoryContainer.Get<ProvinceLevel>().RemoveAll(p => p.ProvinceCode.Equals(provinceCode));
            //获取结果提示
            return base.Message(true, "删除成功");
        }
        /// <summary>
        /// 导出省级行政区列表至Excel文件
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <returns>excel文件结果</returns>
        public FileResult ExportProvinceLevelsToExcel(ProvinceLevelSearcher searcher)
        {
            //获取省级行政区列表Excel文件
            string relativeFileName = searcher.ExportProvinceLevels(_env.WebRootPath);
            //返回文件结果
            return base.File(relativeFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(relativeFileName));
        }

        /// <summary>
        /// 省级行政区
        /// </summary>
        /// <returns>省级行政区</returns>
        [ViewCheckLoginFilter()]
        [Module("reg-management")]
        [Menu("edit-province-levels")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "省级行政区";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑省级行政区
        /// </summary>
        /// <param name="provinceCode">省份代码</param>
        /// <returns>显示编辑省级行政区的分部视图</returns>
        public PartialViewResult ShowEditProvinceLevel(string provinceCode)
        {
            //获取标题
            base.ViewData["Title"] = "编辑省级行政区";
            //获取省级行政区
            ProvinceLevel provinceLevel = RepositoryContainer.Get<ProvinceLevel>().Get(provinceCode);
            //获取分部视图
            return base.PartialView("_EditProvinceLevel", provinceLevel);
        }
        /// <summary>
        /// 显示省级行政区详情
        /// </summary>
        /// <param name="provinceCode">省份代码</param>
        /// <returns>显示省级行政区详情的分部视图</returns>
        public PartialViewResult ShowDetailProvinceLevel(string provinceCode)
        {
            //获取标题
            base.ViewData["Title"] = "省级行政区详情";
            //获取省级行政区
            ProvinceLevel provinceLevel = RepositoryContainer.Get<ProvinceLevel>().Get(provinceCode);
            //获取分部视图
            return base.PartialView("_DetailProvinceLevel", provinceLevel);
        }
        /// <summary>
        /// 分页显示省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示省级行政区列表的分部视图</returns>
        public PartialViewResult ShowListPagedProvinceLevels(ProvinceLevelSearcher searcher, int pageIndex, int pageSize, string functionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            //获取省级行政区分页列表
            IPagedList<ProvinceLevel> provinceLevels = searcher.GetProvinceLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedProvinceLevels", provinceLevels);
        }
        /// <summary>
        /// 分页显示可选省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示可选省级行政区列表的分部视图</returns>
        public PartialViewResult ShowListSelectProvinceLevels(ProvinceLevelSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取省级行政区分页列表
            IPagedList<ProvinceLevel> provinceLevels = searcher.GetProvinceLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSelectProvinceLevels", provinceLevels);
        }
        /// <summary>
        /// 分页显示查询可选省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示查询可选省级行政区列表的分部视图</returns>
        public PartialViewResult ShowListSearchProvinceLevels(ProvinceLevelSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取标题
            base.ViewData["Title"] = "可选省级行政区列表";
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取省级行政区分页列表
            IPagedList<ProvinceLevel> provinceLevels = searcher.GetProvinceLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSearchProvinceLevels", provinceLevels);
        }
    }
}
