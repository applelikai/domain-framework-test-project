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
    /// 县级行政区业务类
    /// </summary>
    internal class CountyLevelService : BaseService, ICountyLevelService
    {
        /// <summary>
        /// 获取县级行政区数据源
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <returns>县级行政区数据源</returns>
        private IDbQuery<CountyLevel> GetCountyLevelQuery(ICountyLevelSearcher searcher)
        {
            //获取省级行政区数据源
            IDbQuery<ProvinceLevel> provinceLevels = base.Query<ProvinceLevel>()
                .IncludeBy(p => p.ProvinceName);
            if (!string.IsNullOrEmpty(searcher.ProvinceName))
                provinceLevels = provinceLevels.Like(p => p.ProvinceName, $"%{searcher.ProvinceName}%");
            //获取地级行政区数据源
            IDbQuery<PrefectureLevel> prefectureLevels = base.Query<PrefectureLevel>()
                .IncludeBy(p => p.PrefectureName);
            if (!string.IsNullOrEmpty(searcher.PrefectureName))
                prefectureLevels = prefectureLevels.Like(p => p.PrefectureName, $"%{searcher.PrefectureName}%");
            //获取县级行政区数据源(并关联省级行政区和地级行政区)
            IDbQuery<CountyLevel> countyLevels = base.Query<CountyLevel>()
                .Join(provinceLevels, c => c.ProvinceLevel, (c, p) => c.ProvinceCode == p.ProvinceCode)
                .LeftJoin(prefectureLevels, c => c.PrefectureLevel, (c, p) => c.PrefectureCode == p.PrefectureCode);
            if (!string.IsNullOrEmpty(searcher.CountyCode))
                countyLevels = countyLevels.Where(c => c.CountyCode.Equals(searcher.CountyCode));
            if (!string.IsNullOrEmpty(searcher.CountyName))
                countyLevels = countyLevels.Like(c => c.CountyName, $"%{searcher.CountyName}%");
            return countyLevels;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public CountyLevelService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取县级行政区列表
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <returns>县级行政区列表</returns>
        public IEnumerable<CountyLevel> GetCountyLevels(ICountyLevelSearcher searcher)
        {
            return this.GetCountyLevelQuery(searcher);
        }
        /// <summary>
        /// 分页获取县级行政区列表
        /// </summary>
        /// <param name="searcher">县级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>县级行政区分页列表</returns>
        public IPagedList<CountyLevel> GetCountyLevels(ICountyLevelSearcher searcher, int pageIndex, int pageSize)
        {
            //获取县级行政区分页数据源
            IDbPagedQuery<CountyLevel> countyLevels = this.GetCountyLevelQuery(searcher)
                .PagingBy(c => c.CountyCode, pageSize, pageIndex);
            //获取县级行政区分页列表
            return new PagedList<CountyLevel>(countyLevels);
        }
    }
}
