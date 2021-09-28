using System;

namespace AutoIHome.Core.Domain.Entities.CfgManagement
{
    /// <summary>
    /// 参数
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string ParameterValue { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 参数分类
        /// </summary>
        public ParameterCategory Category { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序时间
        /// </summary>
        public DateTime? SortedTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public Parameter() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameterName">参数名</param>
        public Parameter(string parameterName)
        {
            this.ParameterName = parameterName;
        }
    }
}
