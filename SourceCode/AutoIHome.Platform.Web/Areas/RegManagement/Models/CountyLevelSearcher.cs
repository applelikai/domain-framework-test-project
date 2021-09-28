using Base.RegManagement.Domain.Models;

namespace AutoIHome.Platform.Web.Areas.RegManagement.Models
{
    /// <summary>
    /// 县级行政区列表查询对象
    /// </summary>
    public class CountyLevelSearcher : ICountyLevelSearcher
    {
        /// <summary>
        /// 省级行政区
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 地级行政区
        /// </summary>
        public string PrefectureName { get; set; }
        /// <summary>
        /// (县级)行政区代码
        /// </summary>
        public string CountyCode { get; set; }
        /// <summary>
        /// (县级)行政区名称
        /// </summary>
        public string CountyName { get; set; }
    }
}
