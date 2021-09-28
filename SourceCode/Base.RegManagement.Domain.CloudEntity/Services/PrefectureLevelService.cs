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
    /// 地级行政区业务类
    /// </summary>
    internal class PrefectureLevelService : BaseService, IPrefectureLevelService
    {
        /// <summary>
        /// 获取地级行政区数据源
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <returns>地级行政区数据源</returns>
        private IDbQuery<PrefectureLevel> GetPrefectureLevelQuery(IPrefectureLevelSearcher searcher)
        {
            //获取省级行政区数据源
            IDbQuery<ProvinceLevel> provinceLevels = base.Query<ProvinceLevel>()
                .IncludeBy(p => p.ProvinceName);
            if (!string.IsNullOrEmpty(searcher.ProvinceName))
                provinceLevels = provinceLevels.Like(p => p.ProvinceName, $"%{searcher.ProvinceName}%");
            //获取地级行政区数据源
            IDbQuery<PrefectureLevel> prefectureLevels = base.Query<PrefectureLevel>()
                .Join(provinceLevels, p => p.ProvinceLevel, (prefecture, province) => prefecture.ProvinceCode == province.ProvinceCode);
            if (!string.IsNullOrEmpty(searcher.PrefectureCode))
                prefectureLevels = prefectureLevels.Where(p => p.PrefectureCode.Equals(searcher.PrefectureCode));
            if (!string.IsNullOrEmpty(searcher.PrefectureName))
                prefectureLevels = prefectureLevels.Like(p => p.PrefectureName, $"%{searcher.PrefectureName}%");
            return prefectureLevels;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public PrefectureLevelService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 获取地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <returns>地级行政区列表</returns>
        public IEnumerable<PrefectureLevel> GetPrefectureLevels(IPrefectureLevelSearcher searcher)
        {
            return this.GetPrefectureLevelQuery(searcher).OrderBy(p => p.PrefectureCode);
        }
        /// <summary>
        /// 分页获取地级行政区列表
        /// </summary>
        /// <param name="searcher">地级行政区列表查询对象</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>地级行政区分页列表</returns>
        public IPagedList<PrefectureLevel> GetPrefectureLevels(IPrefectureLevelSearcher searcher, int pageIndex, int pageSize)
        {
            //获取地级行政区分页数据源
            IDbPagedQuery<PrefectureLevel> prefectureLevels = this.GetPrefectureLevelQuery(searcher)
                .PagingBy(p => p.PrefectureCode, pageSize, pageIndex);
            //获取地级行政区分页列表
            return new PagedList<PrefectureLevel>(prefectureLevels);
        }
    }
}
