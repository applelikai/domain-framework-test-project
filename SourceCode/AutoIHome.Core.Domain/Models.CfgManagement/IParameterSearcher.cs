namespace AutoIHome.Core.Domain.Models.CfgManagement
{
    /// <summary>
    /// 参数列表查询对象
    /// </summary>
    public interface IParameterSearcher
    {
        /// <summary>
        /// 参数分类
        /// </summary>
        string CategoryName { get; }
        /// <summary>
        /// 参数名称
        /// </summary>
        string ParameterName { get; }
    }
}
