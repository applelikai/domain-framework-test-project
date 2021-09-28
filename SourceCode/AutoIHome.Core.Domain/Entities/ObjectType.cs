using System;

namespace AutoIHome.Core.Domain.Entities
{
    /// <summary>
    /// 基础类型
    /// </summary>
    public class ObjectType
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 分类代码
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// 类型分类
        /// </summary>
        public ObjectTypeCategory Category { get; set; }
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
    }
}
