using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Models.EmpManagement;
using AutoIHome.Core.Domain.Services.EmpManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using CloudEntity.Data.Entity;

namespace AutoIHome.Core.Domain.CloudEntity.Services.EmpManagement
{
    /// <summary>
    /// 职位业务类
    /// </summary>
    internal class JobService : BaseService, IJobService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public JobService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="searcher">职位列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>职位分页列表</returns>
        public IPagedList<Job> GetJobs(IJobSearcher searcher, int pageIndex, int pageSize)
        {
            //获取部门关联数据源
            IDbQuery<Department> departments = base.Query<Department>()
                .IncludeBy(d => d.DepartmentName);
            //获取职位预定查询数据源
            IDbQuery<Job> jobs = base.Query<Job>()
                //并关联部门数据源
                .Join(departments, j => j.Department, (j, d) => j.DepartmentId == d.DepartmentId);
            if (!string.IsNullOrEmpty(searcher.DepartmentId))
            {
                //若选择了部门，则获取当前部门下的职位数据源
                jobs = jobs.Where(j => j.DepartmentId.Equals(searcher.DepartmentId));
            }
            if (!string.IsNullOrEmpty(searcher.JobName))
            {
                //若输入了职位名称，则获取包含当前输入的职位名称的职位数据源
                jobs = jobs.Like(j => j.JobName, $"%{searcher.JobName}%");
            }
            //获取职位预定分页列表
            IDbPagedQuery<Job> pagedJobs = jobs.PagingByDescending(j => j.CreatedTime, pageSize, pageIndex);
            return new PagedList<Job>(pagedJobs);
        }
    }
}
