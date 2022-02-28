using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Models.EmpManagement;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;
using System.Collections.Generic;

namespace AutoIHome.Core.Domain.Services.EmpManagement
{
    /// <summary>
    /// 部门业务接口
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 分页获取部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>部门分页列表</returns>
        IPagedList<Department> GetDepartments(IDepartmentSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 部门业务扩展类
    /// </summary>
    public static class DepartmentServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IDepartmentService _Service
        {
            get { return ServiceContainer.Get<IDepartmentService>(); }
        }

        /// <summary>
        /// 分页获取部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>部门分页列表</returns>
        public static IPagedList<Department> GetDepartments(this IDepartmentSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetDepartments(searcher, pageIndex, pageSize);
        }
    }
}
