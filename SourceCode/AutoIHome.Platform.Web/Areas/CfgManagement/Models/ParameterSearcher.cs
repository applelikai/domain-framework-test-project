using AutoIHome.Core.Domain.Models.CfgManagement;

namespace AutoIHome.Platform.Web.Areas.CfgManagement.Models
{
    /// <summary>
    /// 参数列表查询对象
    /// </summary>
    public class ParameterSearcher : IParameterSearcher
    {
        /// <summary>
        /// 参数分类
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
    }
}
