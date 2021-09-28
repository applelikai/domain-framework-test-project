using System;

namespace Domain.Framework.Implementation
{
    /// <summary>
    /// 实现类型所在程序集名称标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class ImplementAssemblyAttribute : Attribute
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public ImplementAssemblyAttribute(string assemblyName)
        {
            this.AssemblyName = assemblyName;
        }
    }
}
