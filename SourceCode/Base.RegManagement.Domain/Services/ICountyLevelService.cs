using AutoIHome.Infrastructure;
using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.Models;
using Base.RegManagement.Domain.OpenXml.Services;
using Domain.Framework.Core.Services;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.Services
{
    /// <summary>
    /// 县级行政区业务接口
    /// </summary>
    public interface ICountyLevelService
    {
        /// <summary>
        /// 获取县级行政区列表
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <returns>县级行政区列表</returns>
        IEnumerable<CountyLevel> GetCountyLevels(ICountyLevelSearcher searcher);
        /// <summary>
        /// 分页获取县级行政区列表
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>县级行政区分页列表</returns>
        IPagedList<CountyLevel> GetCountyLevels(ICountyLevelSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 县级行政区业务扩展类
    /// </summary>
    public static class CountyLevelServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static ICountyLevelService _Service
        {
            get { return ServiceContainer.Get<ICountyLevelService>(); }
        }

        /// <summary>
        /// 导出县级行政区列表至Excel
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <param name="basePath">文件基础路径</param>
        /// <returns>Excel文件相对路径</returns>
        public static string ExportCountyLevels(this ICountyLevelSearcher searcher, string basePath)
        {
            //获取县级行政区列表
            IEnumerable<CountyLevel> countyLevels = _Service.GetCountyLevels(searcher);
            //导出县级行政区列表至Excel
            return ServiceContainer.Get<ICountyLevelExcelService>().ExportCountyLevels(countyLevels, basePath, "县级行政区列表");
        }
        /// <summary>
        /// 分页获取县级行政区列表
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>县级行政区分页列表</returns>
        public static IPagedList<CountyLevel> GetCountyLevels(this ICountyLevelSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetCountyLevels(searcher, pageIndex, pageSize);
        }
    }
}
