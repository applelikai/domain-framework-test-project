using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace Base.RegManagement.Domain.CloudEntity.Framework
{
    /// <summary>
    /// 实体的Mapper基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    internal abstract class BaseMapper<TEntity> : TableMapper<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns>表名</returns>
        private string GetTableName()
        {
            if (base.EntityType.Name.EndsWith("y"))
                return base.EntityType.Name.Replace("y", "ies");
            return string.Concat(base.EntityType.Name, "s");
        }

        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            //获取表名
            string tableName = this.GetTableName();
            //获取Table元数据
            return base.GetHeader(tableName, "public");
        }
    }
}
