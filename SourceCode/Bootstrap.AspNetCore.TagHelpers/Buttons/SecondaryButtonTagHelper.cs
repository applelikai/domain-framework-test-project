using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.TagHelpers.Buttons
{
    /// <summary>
    /// secondary样式按钮标签
    /// </summary>
    public class SecondaryButtonTagHelper : TagHelper
    {
        /// <summary>
        /// 样式类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 点击事件
        /// </summary>
        public string Click { get; set; }

        /// <summary>
        /// 输出标签
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="output">输出</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //指定标签名
            output.TagName = "button";
            //设置属性
            output.Attributes.SetAttribute("class", $"btn {StyleConfigure.GetButtonClassPrefix()}secondary {this.Class}");
            output.Attributes.SetAttribute("type", this.Type ?? "button");
            output.Attributes.SetAttribute("onclick", this.Click);
        }
    }
}
