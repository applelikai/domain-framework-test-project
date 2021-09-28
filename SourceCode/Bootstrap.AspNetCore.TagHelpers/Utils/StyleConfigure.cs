using System;

namespace Bootstrap.AspNetCore.TagHelpers
{
    /// <summary>
    /// 样式配置
    /// </summary>
    public static class StyleConfigure
    {
        /// <summary>
        /// 按钮模式
        /// </summary>
        public static ButtonMode ButtonMode { get; set; }
        /// <summary>
        /// 验证失败提示的模式
        /// </summary>
        public static InvalidMessageMode InvalidMessageMode { get; set; }

        /// <summary>
        /// 静态初始化
        /// </summary>
        static StyleConfigure()
        {
            StyleConfigure.ButtonMode = ButtonMode.Default;
            StyleConfigure.InvalidMessageMode = InvalidMessageMode.Feedback;
        }

        /// <summary>
        /// 获取按钮样式类名称前缀
        /// </summary>
        /// <returns>按钮样式类名称前缀</returns>
        internal static string GetButtonClassPrefix()
        {
            switch (StyleConfigure.ButtonMode)
            {
                //获取边框模式按钮样式类名称前缀
                case ButtonMode.Outline:
                    return "btn-outline-";
                //获取默认模式按钮样式类名称前缀
                case ButtonMode.Default:
                default:
                    return "btn-";
            }
        }
        /// <summary>
        /// 获取提示样式类名称
        /// </summary>
        /// <returns>提示样式类名称</returns>
        internal static string GetInvalidMessageClass()
        {
            switch (StyleConfigure.InvalidMessageMode)
            {
                //获取悬浮框提示样式类名称
                case InvalidMessageMode.Tooltip:
                    return "invalid-tooltip";
                //获取正常反馈提示样式类名称
                case InvalidMessageMode.Feedback:
                default:
                    return "invalid-feedback";
            }
        }
    }
}
