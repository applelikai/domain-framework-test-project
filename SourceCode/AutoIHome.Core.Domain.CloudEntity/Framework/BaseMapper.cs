using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Framework
{
    /// <summary>
    /// Mapper基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    internal abstract class BaseMapper<TEntity> : TableMapper<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 获取表名前缀
        /// </summary>
        /// <returns>表名前缀</returns>
        private string GetTablePrefix()
        {
            //若实体属于公用模块，则直接获取公用模块表前缀名称
            if (base.EntityType.Namespace.EndsWith("Entities"))
                return "Base_";
            //获取模块名称
            string startName = "Entities.";
            int start = base.EntityType.Namespace.IndexOf(startName) + startName.Length;
            int end = base.EntityType.Namespace.IndexOf("Management");
            string moduleName = base.EntityType.Namespace.Substring(start, end - start);
            //获取模块的表前缀名称
            return string.Concat(moduleName, "_");
        }
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
        /// 获取表别名
        /// </summary>
        /// <returns>表别名</returns>
        protected virtual string GetTableAlias()
        {
            return base.EntityType.Name.ToLower();
        }
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            //获取表名
            string tableName = string.Concat(this.GetTablePrefix(), this.GetTableName());
            //获取表别名
            string tableAlias = this.GetTableAlias();
            //获取TableHeader
            return base.GetHeader(tableName, "public", tableAlias);
        }
    }
}
