using AutoIHome.Core.Domain.Entities;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers
{
    /// <summary>
    /// 对象基础类型的Mapper类
    /// </summary>
    internal class ObjectTypeCategoryMapper : TableMapper<ObjectTypeCategory>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Base_ObjectTypeCategories");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<ObjectTypeCategory> setter)
        {
            setter.Map(c => c.CategoryCode, ColumnAction.PrimaryAndInsert).Length(25);
            setter.Map(c => c.CategoryName, allowNull: false).Length(25);
            setter.Map(c => c.Remark).Length(200);
            setter.Map(c => c.SortedTime, ColumnAction.EditAndDefault);
            setter.Map(c => c.CreatedTime, ColumnAction.Default);
        }
    }
}
