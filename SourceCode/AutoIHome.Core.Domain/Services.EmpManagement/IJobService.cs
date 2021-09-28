using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Infrastructure.Models;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;
using System.Collections.Generic;

namespace AutoIHome.Core.Domain.Services.EmpManagement
{
    /// <summary>
    /// 职位业务接口
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <returns>职位列表</returns>
        IEnumerable<Job> GetJobs(INameSearcher searcher);
        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>职位分页列表</returns>
        IPagedList<Job> GetJobs(INameSearcher searcher, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 职位业务扩展类
    /// </summary>
    public static class JobServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IJobService _Service
        {
            get { return ServiceContainer.Get<IJobService>(); }
        }

        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <returns>职位列表</returns>
        public static IEnumerable<Job> GetJobs(this INameSearcher searcher)
        {
            return _Service.GetJobs(searcher);
        }
        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="searcher">名称项查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>职位分页列表</returns>
        public static IPagedList<Job> GetJobs(this INameSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetJobs(searcher, pageIndex, pageSize);
        }
    }
}
