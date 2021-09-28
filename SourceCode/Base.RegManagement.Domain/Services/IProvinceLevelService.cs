using AutoIHome.Infrastructure;
using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.Models;
using Base.RegManagement.Domain.OpenXml.Services;
using Domain.Framework.Core.Services;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.Services
{
    /// <summary>
    /// 省级行政区业务接口
    /// </summary>
    public interface IProvinceLevelService
    {
        /// <summary>
        /// 获取省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <returns>省级行政区列表</returns>
        IEnumerable<ProvinceLevel> GetProvinceLevels(IProvinceLevelSearcher searcher);
        /// <summary>
        /// 分页获取省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>省级行政区分页列表</returns>
        IPagedList<ProvinceLevel> GetProvinceLevels(IProvinceLevelSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 省级行政区业务扩展类
    /// </summary>
    public static class ProvinceLevelServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IProvinceLevelService _Service
        {
            get { return ServiceContainer.Get<IProvinceLevelService>(); }
        }

        /// <summary>
        /// 导出省级行政区列表至Excel
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="basePath">文件基础路径</param>
        /// <returns>Excel文件相对路径</returns>
        public static string ExportProvinceLevels(this IProvinceLevelSearcher searcher, string basePath)
        {
            //获取省级行政区列表
            IEnumerable<ProvinceLevel> provinceLevels = _Service.GetProvinceLevels(searcher);
            //导出省级行政区列表至Excel
            return ServiceContainer.Get<IProvinceLevelExcelService>().ExportProvinceLevels(provinceLevels, basePath, "省级行政区列表");
        }
        /// <summary>
        /// 分页获取省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>省级行政区分页列表</returns>
        public static IPagedList<ProvinceLevel> GetProvinceLevels(this IProvinceLevelSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetProvinceLevels(searcher, pageIndex, pageSize);
        }
    }
}
