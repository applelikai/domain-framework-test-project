using AutoIHome.Core.Domain.Entities.CfgManagement;
using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Core.Domain.Models.SysManagement;
using AutoIHome.Core.Domain.Services.SysManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using CloudEntity.Data.Entity;
using System.Linq;

namespace AutoIHome.Core.Domain.CloudEntity.Services.SysManagement
{
    /// <summary>
    /// 用户业务类
    /// </summary>
    internal class UserService : BaseService, IUserService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public UserService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        public (bool, string) AddUser(User user)
        {
            //检查用户名是否被使用
            if (base.Query<User>().Count(u => u.UserName.Equals(user.UserName)) > 0)
                return (false, $"当前用户名({user.UserName})已被使用");
            //获取默认密码
            string defaultPassword = base.Query<Parameter>(p => p.ParameterName.Equals("base_default-password"))
                .Select(p => p.ParameterValue)
                .SingleOrDefault() ?? "123456";
            //添加用户
            user.Password = Md5Helper.GetMd5(defaultPassword);
            base.List<User>().Add(user);
            //获取结果及提示
            return (true, "添加用户成功");
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>结果及提示</returns>
        public (bool, string) SaveUser(User user)
        {
            //检查用户名是否被其他用户占用
            if (base.Query<User>().Count(u => u.UserName.Equals(user.UserName) && u.UserId != user.UserId) > 0)
                return (false, $"当前用户名({user.UserName})已被使用");
            //保存用户
            base.List<User>().Save(user);
            //获取结果及提示
            return (true, "保存用户成功");
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户</returns>
        public User GetUser(string userName)
        {
            //获取员工数据源
            IDbQuery<Employee> employees = base.Query<Employee>()
                .IncludeBy(e => e.EmployeeName);
            //获取当前用户
            return base.Query<User>()
                .LeftJoin(employees, u => u.Employee, (u, e) => u.EmployeeNo == e.EmployeeNo)
                .SingleOrDefault(e => e.UserName.Equals(userName));
        }
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="searcher">用户列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>用户分页列表</returns>
        public IPagedList<User> GetUsers(IUserSearcher searcher, int pageIndex, int pageSize)
        {
            //获取角色数据源
            IDbQuery<Role> roles = base.Query<Role>()
                .IncludeBy(r => r.RoleName);
            if (!string.IsNullOrEmpty(searcher.RoleName))
                roles = roles.Like(r => r.RoleName, $"%{searcher.RoleName}%");
            //获取员工数据源
            IDbQuery<Employee> employees = base.Query<Employee>()
                .IncludeBy(e => e.EmployeeName);
            if (!string.IsNullOrEmpty(searcher.EmployeeName))
                employees = employees.Like(e => e.EmployeeName, $"%{searcher.EmployeeName}%");
            //获取用户数据源
            IDbQuery<User> users = base.Query<User>()
                .Join(roles, u => u.Role, (u, r) => u.RoleId == r.RoleId)
                .LeftJoin(employees, u => u.Employee, (u, e) => u.EmployeeNo == e.EmployeeNo);
            if (!string.IsNullOrEmpty(searcher.UserName))
                users = users.Like(u => u.UserName, $"%{searcher.UserName}%");
            //获取用户分页列表
            IDbPagedQuery<User> pagedUsers = users.PagingByDescending(u => u.CreatedTime, pageSize, pageIndex);
            return new PagedList<User>(pagedUsers);
        }
    }
}
