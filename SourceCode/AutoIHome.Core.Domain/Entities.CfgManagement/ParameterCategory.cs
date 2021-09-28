using System;

namespace AutoIHome.Core.Domain.Entities.CfgManagement
{
    /// <summary>
    /// 参数分类
    /// </summary>
    public class ParameterCategory
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
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
        public ParameterCategory() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="categoryId">分类id</param>
        public ParameterCategory(string categoryId)
        {
            this.CategoryId = categoryId;
        }
    }
}
