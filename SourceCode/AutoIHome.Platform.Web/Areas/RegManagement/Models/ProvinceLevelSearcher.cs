using Base.RegManagement.Domain.Models;

namespace AutoIHome.Platform.Web.Areas.RegManagement.Models
{
    /// <summary>
    /// 省级行政区列表查询对象
    /// </summary>
    public class ProvinceLevelSearcher : IProvinceLevelSearcher
    {
        /// <summary>
        /// 省级行政区代码
        /// </summary>
        public string ProvinceCode { get; set; }
        /// <summary>
        /// 省级行政区名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 行政区类型
        /// </summary>
        public string ProvinceType { get; set; }
    }
}
