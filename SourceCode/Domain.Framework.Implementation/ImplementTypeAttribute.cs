using System;

namespace Domain.Framework.Implementation
{
    /// <summary>
    /// 实现类标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class ImplementTypeAttribute : Attribute
    {
        /// <summary>
        /// 类型全名
        /// </summary>
        public string TypeFullName { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="typeFullName">类型全名</param>
        public ImplementTypeAttribute(string typeFullName)
        {
            this.TypeFullName = typeFullName;
        }
    }
}
