using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using Base.RegManagement.Domain.Entities;
using Base.RegManagement.Domain.Models;
using Base.RegManagement.Domain.Services;
using CloudEntity.Data.Entity;
using System.Collections.Generic;

namespace Base.RegManagement.Domain.CloudEntity.Services
{
    /// <summary>
    /// 省级行政区业务类
    /// </summary>
    internal class ProvinceLevelService : BaseService, IProvinceLevelService
    {
        /// <summary>
        /// 获取省级行政区数据源
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <returns>省级行政区数据源</returns>
        private IDbQuery<ProvinceLevel> GetProvinceLevelQuery(IProvinceLevelSearcher searcher)
        {
            //初始化省级行政区数据源
            IDbQuery<ProvinceLevel> provinceLevels = base.Query<ProvinceLevel>();
            //构建查询条件
            if (!string.IsNullOrEmpty(searcher.ProvinceCode))
                provinceLevels = provinceLevels.Where(p => p.ProvinceCode.Equals(searcher.ProvinceCode));
            if (!string.IsNullOrEmpty(searcher.ProvinceType))
                provinceLevels = provinceLevels.Where(p => p.ProvinceType.Equals(searcher.ProvinceType));
            if (!string.IsNullOrEmpty(searcher.ProvinceName))
                provinceLevels = provinceLevels.Like(p => p.ProvinceName, $"%{searcher.ProvinceName}%");
            //获取省级行政区数据源
            return provinceLevels;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ProvinceLevelService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <returns>省级行政区列表</returns>
        public IEnumerable<ProvinceLevel> GetProvinceLevels(IProvinceLevelSearcher searcher)
        {
            return this.GetProvinceLevelQuery(searcher).OrderBy(p => p.ProvinceCode);
        }
        /// <summary>
        /// 分页获取省级行政区列表
        /// </summary>
        /// <param name="searcher">省级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>省级行政区分页列表</returns>
        public IPagedList<ProvinceLevel> GetProvinceLevels(IProvinceLevelSearcher searcher, int pageIndex, int pageSize)
        {
            //获取省级行政区分页数据源
            IDbPagedQuery<ProvinceLevel> provinceLevels = this.GetProvinceLevelQuery(searcher)
                .PagingBy(p => p.ProvinceCode, pageSize, pageIndex);
            //获取省级行政区分页列表
            return new PagedList<ProvinceLevel>(provinceLevels);
        }
    }
}
