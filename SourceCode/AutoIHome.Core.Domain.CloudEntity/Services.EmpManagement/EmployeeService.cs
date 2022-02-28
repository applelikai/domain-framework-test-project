using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Models.EmpManagement;
using AutoIHome.Core.Domain.Services.EmpManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using CloudEntity.Data.Entity;
using System.Collections.Generic;

namespace AutoIHome.Core.Domain.CloudEntity.Services.EmpManagement
{
    /// <summary>
    /// 员工业务类
    /// </summary>
    internal class EmployeeService : BaseService, IEmployeeService
    {
        /// <summary>
        /// 获取员工数据源
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <returns>员工数据源</returns>
        private IDbQuery<Employee> GetEmployeeQuery(IEmployeeSearcher searcher)
        {
            //获取部门数据源
            IDbQuery<Department> departments = base.Query<Department>()
                .IncludeBy(d => d.DepartmentName);
            if (!string.IsNullOrEmpty(searcher.DepartmentName))
                departments = departments.Like(d => d.DepartmentName, $"%{searcher.DepartmentName}%");
            //获取职位数据源
            IDbQuery<Job> jobs = base.Query<Job>()
                .IncludeBy(j => j.JobName);
            if (!string.IsNullOrEmpty(searcher.JobName))
                jobs = jobs.Like(j => j.JobName, $"%{searcher.JobName}%");
            //获取员工数据源
            IDbQuery<Employee> employees = base.Query<Employee>()
                .LeftJoin(departments, e => e.Department, (e, d) => e.DepartmentId == d.DepartmentId)
                .LeftJoin(jobs, e => e.Job, (e, j) => e.JobId == j.JobId);
            if (!string.IsNullOrEmpty(searcher.EmployeeName))
                employees = employees.Like(e => e.EmployeeName, $"%{searcher.EmployeeName}%");
            if (!string.IsNullOrEmpty(searcher.PhoneNumber))
                employees = employees.Like(e => e.PhoneNumber, $"%{searcher.PhoneNumber}%");
            if (searcher.IsDeleted != null)
                employees = employees.Where(e => e.IsDeleted == searcher.IsDeleted);
            return employees;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public EmployeeService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <returns>员工列表</returns>
        public IEnumerable<Employee> GetEmployees(IEmployeeSearcher searcher)
        {
            //获取员工数据源
            return this.GetEmployeeQuery(searcher);
        }
        /// <summary>
        /// 分页获取员工列表
        /// </summary>
        /// <param name="searcher">员工列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>员工分页列表</returns>
        public IPagedList<Employee> GetEmployees(IEmployeeSearcher searcher, int pageIndex, int pageSize)
        {
            //获取员工数据源
            IDbQuery<Employee> employees = this.GetEmployeeQuery(searcher);
            //获取员工分页列表
            IDbPagedQuery<Employee> pagedEmployees = employees.PagingByDescending(e => e.CreatedTime, pageSize, pageIndex);
            return new PagedList<Employee>(pagedEmployees);
        }
    }
}
