namespace Base.RegManagement.Domain.Models
{
    /// <summary>
    /// 地级行政区列表查询对象
    /// </summary>
    public interface IPrefectureLevelSearcher
    {
        /// <summary>
        /// 省级行政区
        /// </summary>
        string ProvinceName { get; }
        /// <summary>
        /// 地级行政区代码
        /// </summary>
        string PrefectureCode { get; }
        /// <summary>
        /// 地级行政区名称
        /// </summary>
        string PrefectureName { get; }
    }
}
