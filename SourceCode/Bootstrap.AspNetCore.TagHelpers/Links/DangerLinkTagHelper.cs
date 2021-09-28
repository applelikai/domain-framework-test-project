namespace Bootstrap.AspNetCore.TagHelpers.Links
{
    /// <summary>
    /// text-danger样式的a标签
    /// </summary>
    public class DangerLinkTagHelper : LinkTagHelper
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DangerLinkTagHelper()
            : base("text-danger") { }
    }
}
