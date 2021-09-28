using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrap.AspNetCore.TagHelpers.Tabs
{
    /// <summary>
    /// bootstrap中充当tab角色的按钮
    /// </summary>
    public class TabButtonTagHelper : TagHelper
    {
        /// <summary>
        /// 样式类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 名称(id:tab-{名称} href:#tab-pane-{名称})
        /// </summary>
        public string Name { get; set; }
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
            //设置type
            output.Attributes.SetAttribute("type", "button");
            //设置类样式
            string className = string.IsNullOrEmpty(this.Class) ? string.Empty : $" {this.Class}";
            output.Attributes.SetAttribute("class", $"nav-link{className}");
            //设置标签角色
            output.Attributes.SetAttribute("data-bs-toggle", "tab");
            output.Attributes.SetAttribute("role", "tab");
            //设置id
            output.Attributes.SetAttribute("id", $"tab-{this.Name}");
            //设置跳转目标
            output.Attributes.SetAttribute("data-bs-target", $"#tab-pane-{this.Name}");
            output.Attributes.SetAttribute("aria-controls", $"tab-pane-{this.Name}");
            //设置是否选中
            output.Attributes.SetAttribute("aria-selected", ("active".Equals(this.Class)).ToString().ToLower());
            //设置点击事件
            if (!string.IsNullOrWhiteSpace(this.Click))
                output.Attributes.SetAttribute("onclick", this.Click);
        }
    }
}
