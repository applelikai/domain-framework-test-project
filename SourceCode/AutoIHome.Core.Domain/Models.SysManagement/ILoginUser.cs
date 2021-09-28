namespace AutoIHome.Core.Domain.Models.SysManagement
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public interface ILoginUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; }
    }
    /// <summary>
    /// 登录用户扩展类
    /// </summary>
    public static class LoginUserExtender
    {
        /// <summary>
        /// 非空检查
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>检查结果</returns>
        public static bool CheckNull(this ILoginUser loginUser, ref string errorMessage)
        {
            //检查用户名是否为空
            if (string.IsNullOrEmpty(loginUser.UserName))
            {
                errorMessage = "请输入用户名";
                return false;
            }
            //检查密码是否为空
            if (string.IsNullOrEmpty(loginUser.Password))
            {
                errorMessage = "请输入密码";
                return false;
            }
            //检查通过
            return true;
        }
    }
}
