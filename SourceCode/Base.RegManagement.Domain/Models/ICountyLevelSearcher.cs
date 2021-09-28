namespace Base.RegManagement.Domain.Models
{
    /// <summary>
    /// 县级行政区列表查询对象
    /// </summary>
    public interface ICountyLevelSearcher
    {
        /// <summary>
        /// 省级行政区
        /// </summary>
        string ProvinceName { get; }
        /// <summary>
        /// 地级行政区
        /// </summary>
        string PrefectureName { get; }
        /// <summary>
        /// (县级)行政区代码
        /// </summary>
        string CountyCode { get; }
        /// <summary>
        /// (县级)行政区名称
        /// </summary>
        string CountyName { get; }
    }
}
