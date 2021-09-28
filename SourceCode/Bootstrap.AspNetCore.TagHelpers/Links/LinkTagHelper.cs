using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.TagHelpers.Links
{
    /// <summary>
    /// bootstrap样式的a标签
    /// </summary>
    public abstract class LinkTagHelper : TagHelper
    {
        /// <summary>
        /// 内置的样式类名
        /// </summary>
        private string _className;

        /// <summary>
        /// 用户添加的样式类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 点击事件
        /// </summary>
        public string Click { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="className">内置类名</param>
        public LinkTagHelper(string className)
        {
            _className = className;
        }
        /// <summary>
        /// 输出标签
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="output">输出</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //指定标签名
            output.TagName = "a";
            //设置样式类名
            if (!string.IsNullOrEmpty(this.Class))
                output.Attributes.SetAttribute("class", $"{_className} {this.Class}");
            else
                output.Attributes.SetAttribute("class", _className);
            //设置链接地址
            output.Attributes.SetAttribute("href", this.Href ?? "javascript:void(0);");
            //设置点击事件
            if (!string.IsNullOrEmpty(this.Click))
                output.Attributes.SetAttribute("onclick", this.Click);
        }
    }
}
