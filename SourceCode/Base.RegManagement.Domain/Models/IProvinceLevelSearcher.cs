namespace Base.RegManagement.Domain.Models
{
    /// <summary>
    /// 省级行政区列表查询对象
    /// </summary>
    public interface IProvinceLevelSearcher
    {
        /// <summary>
        /// 省级行政区代码
        /// </summary>
        string ProvinceCode { get; }
        /// <summary>
        /// 省级行政区名称
        /// </summary>
        string ProvinceName { get; }
        /// <summary>
        /// 行政区类型
        /// </summary>
        string ProvinceType { get; }
    }
}
