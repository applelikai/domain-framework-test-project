using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Core.Domain.Models.SysManagement;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services.SysManagement
{
    /// <summary>
    /// 用户业务接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        (bool, string) AddUser(User user);
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        (bool, string) SaveUser(User user);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户</returns>
        User GetUser(string userName);
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="searcher">用户列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>用户分页列表</returns>
        IPagedList<User> GetUsers(IUserSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 用户业务扩展类
    /// </summary>
    public static class UserServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IUserService _Service
        {
            get { return ServiceContainer.Get<IUserService>(); }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        public static (bool, string) Register(this User user)
        {
            return _Service.AddUser(user);
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        public static (bool, string) Save(this User user)
        {
            return _Service.SaveUser(user);
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="loginUser">登录用户</param>
        /// <returns>用户</returns>
        public static User GetUser(this ILoginUser loginUser)
        {
            return _Service.GetUser(loginUser.UserName);
        }
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="searcher">用户列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>用户分页列表</returns>
        public static IPagedList<User> GetUsers(this IUserSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetUsers(searcher, pageIndex, pageSize);
        }
    }
}
