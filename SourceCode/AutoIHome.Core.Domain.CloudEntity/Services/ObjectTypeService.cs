using AutoIHome.Core.Domain.Entities;
using AutoIHome.Core.Domain.Services;
using AutoIHome.Infrastructure;
using AutoIHome.Infrastructure.CloudEntity;
using AutoIHome.Infrastructure.Framework.Services;
using CloudEntity.Data.Entity;

namespace AutoIHome.Core.Domain.CloudEntity.Services
{
    /// <summary>
    /// 基础类型业务类
    /// </summary>
    internal class ObjectTypeService : BaseService, IObjectTypeService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container">数据容器</param>
        public ObjectTypeService(IDbContainer container)
            : base(container) { }
        /// <summary>
        /// 分页获取当前分类下的基础类型列表
        /// </summary>
        /// <param name="category">基础类型分类</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>基础类型分页列表</returns>
        public IPagedList<ObjectType> GetObjectTypes(ObjectTypeCategory category, int pageIndex, int pageSize)
        {
            //获取基础类型数据源
            IDbQuery<ObjectType> objectTypes = base.Query<ObjectType>(t => t.CategoryCode.Equals(category.CategoryCode));
            //获取基础类型分页列表
            IDbPagedQuery<ObjectType> pagedObjectTypes = objectTypes.PagingByDescending(t => t.SortedTime, pageSize, pageIndex);
            return new PagedList<ObjectType>(pagedObjectTypes, t => t.Category = category);
        }
    }
}
