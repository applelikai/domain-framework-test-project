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
    /// 地级行政区控制器
    /// </summary>
    [Area("RegManagement")]
    [Route("RegManagement/[controller]/[action]")]
    public class PrefectureLevelController : EntityController<PrefectureLevel>
    {
        /// <summary>
        /// web host环境对象
        /// </summary>
        private IWebHostEnvironment _env;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="env">web host环境对象</param>
        public PrefectureLevelController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 删除地级行政区
        /// </summary>
        /// <param name="prefectureCode">地级行政区代码</param>
        /// <returns>结果及提示</returns>
        public JsonResult RemovePrefectureLevel(string prefectureCode)
        {
            //检查当前地级行政区下有没有县级行政区
            int countyLevelCount = RepositoryContainer.Get<CountyLevel>().GetCount(c => c.PrefectureCode.Equals(prefectureCode));
            if (countyLevelCount > 0)
                return base.Message(false, "当前地级行政区下有县级行政区，不可删除");
            //删除地级行政区
            RepositoryContainer.Get<PrefectureLevel>().RemoveAll(p => p.PrefectureCode.Equals(prefectureCode));
            //获取结果提示
            return base.Message(true, "删除成功");
        }
        /// <summary>
        /// 导出地级行政区列表至Excel文件
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <returns>excel文件结果</returns>
        public FileResult ExportPrefectureLevelsToExcel(PrefectureLevelSearcher searcher)
        {
            //获取地级行政区列表Excel文件
            string relativeFileName = searcher.ExportPrefectureLevels(_env.WebRootPath);
            //获取文件结果
            return base.File(relativeFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(relativeFileName));
        }

        /// <summary>
        /// 地级行政区
        /// </summary>
        /// <returns>地级行政区</returns>
        [ViewCheckLoginFilter()]
        [Module("reg-management")]
        [Menu("edit-prefecture-levels")]
        public ViewResult Index()
        {
            //获取标题
            base.ViewData["Title"] = "地级行政区";
            //获取视图
            return base.View();
        }

        /// <summary>
        /// 显示编辑地级行政区
        /// </summary>
        /// <param name="prefectureCode">地级行政区代码</param>
        /// <returns>显示编辑地级行政区的分部视图</returns>
        public PartialViewResult ShowEditPrefectureLevel(string prefectureCode)
        {
            //获取标题
            base.ViewData["Title"] = "编辑地级行政区";
            //获取地级行政区
            PrefectureLevel prefectureLevel = RepositoryContainer.Get<PrefectureLevel>().Get(prefectureCode);
            prefectureLevel.ProvinceLevel = RepositoryContainer.Get<ProvinceLevel>().Get(prefectureLevel.ProvinceCode);
            //获取分部视图
            return base.PartialView("_EditPrefectureLevel", prefectureLevel);
        }
        /// <summary>
        /// 显示地级行政区详情
        /// </summary>
        /// <param name="prefectureCode">地级行政区代码</param>
        /// <returns>显示地级行政区详情的分部视图</returns>
        public PartialViewResult ShowDetailPrefectureLevel(string prefectureCode)
        {
            //获取标题
            base.ViewData["Title"] = "地级行政区详情";
            //获取地级行政区
            PrefectureLevel prefectureLevel = RepositoryContainer.Get<PrefectureLevel>().Get(prefectureCode);
            prefectureLevel.ProvinceLevel = RepositoryContainer.Get<ProvinceLevel>().Get(prefectureLevel.ProvinceCode);
            //获取分部视图
            return base.PartialView("_DetailPrefectureLevel", prefectureLevel);
        }
        /// <summary>
        /// 分页显示地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <returns>分页显示地级行政区列表的分部视图</returns>
        public PartialViewResult ShowListPagedPrefectureLevels(PrefectureLevelSearcher searcher, int pageIndex, int pageSize, string functionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            //获取地级行政区分页列表
            IPagedList<PrefectureLevel> prefectureLevels = searcher.GetPrefectureLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListPagedPrefectureLevels", prefectureLevels);
        }
        /// <summary>
        /// 分页显示可选地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示可选地级行政区列表的分部视图</returns>
        public PartialViewResult ShowListSelectPrefectureLevels(PrefectureLevelSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取地级行政区分页列表
            IPagedList<PrefectureLevel> prefectureLevels = searcher.GetPrefectureLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSelectPrefectureLevels", prefectureLevels);
        }
        /// <summary>
        /// 分页显示查询可选地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <param name="functionName">页面查询函数名称</param>
        /// <param name="selectFunctionName">选择元素函数的名称</param>
        /// <returns>分页显示查询可选地级行政区列表的分部视图</returns>
        public PartialViewResult ShowListSearchPrefectureLevels(PrefectureLevelSearcher searcher, int pageIndex, int pageSize, string functionName, string selectFunctionName)
        {
            //获取标题
            base.ViewData["Title"] = "可选地级行政区列表";
            //获取查询表单参数
            base.ViewData["ProvinceName"] = searcher.ProvinceName;
            //获取参数
            base.ViewBag.Function = functionName;
            base.ViewBag.SelectFunction = selectFunctionName;
            //获取地级行政区分页列表
            IPagedList<PrefectureLevel> prefectureLevels = searcher.GetPrefectureLevels(pageIndex, pageSize);
            //获取分部视图
            return base.PartialView("_ListSearchPrefectureLevels", prefectureLevels);
        }
    }
}
