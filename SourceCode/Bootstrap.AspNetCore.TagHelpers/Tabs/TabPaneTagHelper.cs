using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bootstrap.AspNetCore.TagHelpers.Tabs
{
    /// <summary>
    /// bootstrap中充当tab-pane角色的div标签
    /// </summary>
    public class TabPaneTagHelper : TagHelper
    {
        /// <summary>
        /// 样式类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 名称(id:tab-pane-{名称} aria-labelledby:#tab-{名称})
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 输出标签
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="output">输出</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //指定标签名
            output.TagName = "div";
            //设置标签角色
            output.Attributes.SetAttribute("role", "tabpanel");
            //设置类样式
            string className = string.IsNullOrEmpty(this.Class) ? string.Empty : $" {this.Class}";
            output.Attributes.SetAttribute("class", $"tab-pane fade{className}");
            //设置id
            output.Attributes.SetAttribute("id", $"tab-pane-{this.Name}");
            //绑定目标tab的id
            output.Attributes.SetAttribute("aria-labelledby", $"tab-{this.Name}");
        }
    }
}
