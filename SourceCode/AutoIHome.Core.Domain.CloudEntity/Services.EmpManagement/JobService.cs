using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Services.EmpManagement;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using AutoIHome.Infrastructure.Models;
using CloudEntity.Data.Entity;
using System.Collections.Generic;

namespace AutoIHome.Core.Domain.CloudEntity.Services.EmpManagement
{
    /// <summary>
    /// 职位业务类
    /// </summary>
    internal class JobService : BaseService, IJobService
    {
        /// <summary>
        /// 获取职位数据源
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <returns>职位数据源</returns>
        private IDbQuery<Job> GetJobQuery(INameSearcher searcher)
        {
            //初始化职位数据源,默认预定所有职位(预定时不执行查询，仅构建查询条件)
            IDbQuery<Job> jobs = base.Query<Job>();
            //添加条件缩小数据预定范围
            if (!string.IsNullOrEmpty(searcher.SearchName))
                jobs = jobs.Like(j => j.JobName, $"%{searcher.SearchName}%");
            //获取职位数据源
            return jobs;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public JobService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <returns>职位列表</returns>
        public IEnumerable<Job> GetJobs(INameSearcher searcher)
        {
            //获取职位数据源
            return this.GetJobQuery(searcher);
        }
        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>职位分页列表</returns>
        public IPagedList<Job> GetJobs(INameSearcher searcher, int pageIndex, int pageSize)
        {
            //获取职位数据源
            IDbQuery<Job> jobs = this.GetJobQuery(searcher);
            //获取职位分页列表
            IDbPagedQuery<Job> pagedJobs = jobs.PagingByDescending(j => j.CreatedTime, pageSize, pageIndex);
            return new PagedList<Job>(pagedJobs);
        }
    }
}
