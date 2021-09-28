using System;

namespace AutoIHome.Core.Domain.Entities
{
    /// <summary>
    /// 基础类型分类
    /// </summary>
    public class ObjectTypeCategory
    {
        /// <summary>
        /// 分类代码
        /// </summary>
        public string CategoryCode { get; set; }
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
        /// 创建对象类型分类
        /// </summary>
        public ObjectTypeCategory() { }
        /// <summary>
        /// 创建对象类型分类
        /// </summary>
        /// <param name="categoryCode">分类代码</param>
        public ObjectTypeCategory(string categoryCode)
        {
            this.CategoryCode = categoryCode;
        }
    }
}
