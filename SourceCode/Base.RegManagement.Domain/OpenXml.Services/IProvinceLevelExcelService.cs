using Base.RegManagement.Domain.Entities;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.OpenXml.Services
{
    /// <summary>
    /// 省级行政区Excel业务接口
    /// </summary>
    public interface IProvinceLevelExcelService
    {
        /// <summary>
        /// 导出省级行政区列表至Excel
        /// </summary>
        /// <param name="provinceLevels">省级行政区列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        string ExportProvinceLevels(IEnumerable<ProvinceLevel> provinceLevels, string basePath, string title);
    }
}
