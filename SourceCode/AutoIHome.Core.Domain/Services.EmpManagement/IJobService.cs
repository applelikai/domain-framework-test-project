using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Models.EmpManagement;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services.EmpManagement
{
    /// <summary>
    /// 职位业务接口
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="searcher">职位列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>职位分页列表</returns>
        IPagedList<Job> GetJobs(IJobSearcher searcher, int pageIndex, int pageSize);
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
        /// 分页获取职位列表
        /// </summary>
        /// <param name="searcher">职位列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>职位分页列表</returns>
        public static IPagedList<Job> GetJobs(this IJobSearcher searcher, int pageIndex, int pageSize)
        {
            return _Service.GetJobs(searcher, pageIndex, pageSize);
        }
    }
}
