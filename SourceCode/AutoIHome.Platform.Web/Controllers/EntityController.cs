using Domain.Framework.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoIHome.Platform.Web.Controllers
{
    /// <summary>
    /// 实体控制器基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class EntityController<TEntity> : BaseController
        where TEntity : class
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果提示</returns>
        public virtual JsonResult AddEntity(TEntity entity)
        {
            //添加实体
            RepositoryContainer.Get<TEntity>().Add(entity);
            //获取结果提示
            return base.Message(true, "添加成功");
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果提示</returns>
        public virtual JsonResult SaveEntity(TEntity entity)
        {
            //保存实体
            RepositoryContainer.Get<TEntity>().Save(entity);
            //获取结果提示
            return base.Message(true, "保存成功");
        }
    }
}
