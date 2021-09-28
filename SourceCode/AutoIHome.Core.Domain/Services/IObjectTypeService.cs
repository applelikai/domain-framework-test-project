using AutoIHome.Core.Domain.Entities;
using AutoIHome.Infrastructure;
using Domain.Framework.Core.Services;

namespace AutoIHome.Core.Domain.Services
{
    /// <summary>
    /// 基础类型业务接口
    /// </summary>
    public interface IObjectTypeService
    {
        /// <summary>
        /// 分页获取当前分类下的基础类型列表
        /// </summary>
        /// <param name="category">基础类型分类</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>基础类型分页列表</returns>
        IPagedList<ObjectType> GetObjectTypes(ObjectTypeCategory category, int pageIndex, int pageSize);
    }
    /// <summary>
    /// 基础类型业务扩展类
    /// </summary>
    public static class ObjectTypeServiceExtender
    {
        /// <summary>
        /// 业务处理对象
        /// </summary>
        private static IObjectTypeService _Service
        {
            get { return ServiceContainer.Get<IObjectTypeService>(); }
        }

        /// <summary>
        /// 分页获取当前分类下的基础类型列表
        /// </summary>
        /// <param name="category">基础类型分类</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页元素数量</param>
        /// <returns>基础类型分页列表</returns>
        public static IPagedList<ObjectType> GetObjectTypes(this ObjectTypeCategory category, int pageIndex, int pageSize)
        {
            return _Service.GetObjectTypes(category, pageIndex, pageSize);
        }
    }
}
