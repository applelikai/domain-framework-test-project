using Base.RegManagement.Domain.Entities;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.OpenXml.Services
{
    /// <summary>
    /// 县级行政区Excel业务接口
    /// </summary>
    public interface ICountyLevelExcelService
    {
        /// <summary>
        /// 导出县级行政区列表至Excel
        /// </summary>
        /// <param name="countyLevels">县级行政区列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        string ExportCountyLevels(IEnumerable<CountyLevel> countyLevels, string basePath, string title);
    }
}
