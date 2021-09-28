using AutoIHome.Infrastructure;
using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.Models;
using Base.RegManagement.Domain.OpenXml.Services;
using Domain.Framework.Core.Services;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.Services
{
    /// <summary>
    /// 地级行政区业务接口
    /// </summary>
    public interface IPrefectureLevelService
    {
        /// <summary>
        /// 获取地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <returns>地级行政区列表</returns>
        IEnumerable<PrefectureLevel> GetPrefectureLevels(IPrefectureLevelSearcher searcher);
        /// <summary>
        /// 分页获取地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>地级行政区分页列表</returns>
        IPagedList<PrefectureLevel> GetPrefectureLevels(IPrefectureLevelSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 地级行政区业务扩展类
    /// </summary>
    public static class PrefectureLevelServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IPrefectureLevelService _Service
        {
            get { return ServiceContainer.Get<IPrefectureLevelService>(); }
        }

        /// <summary>
        /// 导出地级行政区列表至Excel
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="basePath">文件基础路径</param>
        /// <returns>Excel文件相对路径</returns>
        public static string ExportPrefectureLevels(this IPrefectureLevelSearcher searcher, string basePath)
        {
            //获取地级行政区列表
            IEnumerable<PrefectureLevel> prefectureLevels = _Service.GetPrefectureLevels(searcher);
            //导出地级行政区列表至Excel
            return ServiceContainer.Get<IPrefectureLevelExcelService>().ExportPrefectureLevels(prefectureLevels, basePath, "地级行政区列表");
        }
        /// <summary>
        /// 分页获取地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>地级行政区分页列表</returns>
        public static IPagedList<PrefectureLevel> GetPrefectureLevels(this IPrefectureLevelSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetPrefectureLevels(searcher, pageIndex, pageSize);
        }
    }
}
