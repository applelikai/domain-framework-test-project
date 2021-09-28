using Base.RegManagement.Domain.Models;

namespace AutoIHome.Platform.Web.Areas.RegManagement.Models
{
    /// <summary>
    /// 地级行政区列表查询对象
    /// </summary>
    public class PrefectureLevelSearcher : IPrefectureLevelSearcher
    {
        /// <summary>
        /// 省级行政区
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 地级行政区代码
        /// </summary>
        public string PrefectureCode { get; set; }
        /// <summary>
        /// 地级行政区名称
        /// </summary>
        public string PrefectureName { get; set; }
    }
}
