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
    /// 部门业务类
    /// </summary>
    internal class DepartmentService : BaseService, IDepartmentService
    {
        /// <summary>
        /// 获取部门数据源
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <returns>部门数据源</returns>
        private IDbQuery<Department> GetDepartmentQuery(IDepartmentSearcher searcher)
        {
            //初始化部门数据源,默认预定所有部门(预定时不执行查询，仅构建查询条件)
            IDbQuery<Department> departments = base.Query<Department>();
            //添加条件缩小数据预定范围
            if (!string.IsNullOrEmpty(searcher.DepartmentNo))
                departments = departments.Where(d => d.DepartmentNo.Equals(searcher.DepartmentNo));
            if (!string.IsNullOrEmpty(searcher.DepartmentName))
                departments = departments.Like(d => d.DepartmentName, $"%{searcher.DepartmentName}%");
            //获取部门数据源
            return departments;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public DepartmentService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <returns>部门列表</returns>
        public IEnumerable<Department> GetDepartments(IDepartmentSearcher searcher)
        {
            //获取部门数据源
            return this.GetDepartmentQuery(searcher);
        }
        /// <summary>
        /// 分页获取部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>部门分页列表</returns>
        public IPagedList<Department> GetDepartments(IDepartmentSearcher searcher, int pageIndex, int pageSize)
        {
            //获取部门数据源
            IDbQuery<Department> departments = this.GetDepartmentQuery(searcher);
            //获取部门分页列表
            IDbPagedQuery<Department> pagedDepartments = departments.PagingByDescending(d => d.CreatedTime, pageSize, pageIndex);
            return new PagedList<Department>(pagedDepartments);
        }
    }
}
