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
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public DepartmentService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 分页获取部门列表
        /// </summary>
        /// <param name="searcher">部门列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>部门分页列表</returns>
        public IPagedList<Department> GetDepartments(IDepartmentSearcher searcher, int pageIndex, int pageSize)
        {
            //获取上级部门预定关联查询数据源
            IDbQuery<DepartmentInfo> parents = base.Query<DepartmentInfo>();
            if (string.IsNullOrEmpty(searcher.ParentId) && !string.IsNullOrEmpty(searcher.ParentName))
            {
                //若是手动输入上级部门名称,则添加上级部门预定(模糊)查询条件
                parents = parents.Like(p => p.DepartmentInfoName, $"%{searcher.ParentName}%");
            }
            //初始化部门预定查询数据源(预定时不执行查询，仅构建查询条件)
            IDbQuery<Department> departments = base.Query<Department>()
                //关联上级部门
                .LeftJoin(parents, d => d.Parent, (d, p) => d.ParentId == p.DepartmentInfoId);
            if (!string.IsNullOrEmpty(searcher.ParentId))
            {
                //若选择了上级部门，添加预定查询条件：上级部门id匹配
                departments = departments.Where(d => d.ParentId.Equals(searcher.ParentId));
            }
            if (!string.IsNullOrEmpty(searcher.DepartmentName))
            {
                //若输入了部门名称，添加预定查询条件：部门名称包含当前输入的部门名称
                departments = departments.Like(d => d.DepartmentName, $"%{searcher.DepartmentName}%");
            }
            //获取部门预定分页查询列表
            IDbPagedQuery<Department> pagedDepartments = departments.PagingByDescending(d => d.CreatedTime, pageSize, pageIndex);
            return new PagedList<Department>(pagedDepartments);
        }
    }
}
