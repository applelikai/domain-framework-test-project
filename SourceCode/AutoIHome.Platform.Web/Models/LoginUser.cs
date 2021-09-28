using AutoIHome.Core.Domain.Models.SysManagement;

namespace AutoIHome.Platform.Web.Models
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUser : ILoginUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
