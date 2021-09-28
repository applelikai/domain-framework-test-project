using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.TagHelpers.Form
{
    /// <summary>
    /// 验证失败提示标签
    /// </summary>
    public class InvalidMessageTagHelper : TagHelper
    {
        /// <summary>
        /// 样式类名
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 输出标签
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="output">输出</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //指定标签名
            output.TagName = "div";
            //设置属性
            output.Attributes.SetAttribute("class", $"{StyleConfigure.GetInvalidMessageClass()} {this.Class}");
        }
    }
}
