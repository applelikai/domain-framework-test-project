using Base.RegManagement.Domain.Entities;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.OpenXml.Services
{
    /// <summary>
    /// 地级行政区Excel业务接口
    /// </summary>
    public interface IPrefectureLevelExcelService
    {
        /// <summary>
        /// 导出地级行政区列表至Excel
        /// </summary>
        /// <param name="prefectureLevels">地级行政区列表</param>
        /// <param name="basePath">文件基础路径</param>
        /// <param name="title">标题</param>
        /// <returns>Excel文件相对路径</returns>
        string ExportPrefectureLevels(IEnumerable<PrefectureLevel> prefectureLevels, string basePath, string title);
    }
}
