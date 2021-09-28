using AutoIHome.Core.Domain.Entities;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers
{
    /// <summary>
    /// 基础类型的Mapper类
    /// </summary>
    internal class ObjectTypeMapper : TableMapper<ObjectType>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Base_ObjectTypes");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<ObjectType> setter)
        {
            setter.Map(t => t.TypeId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(t => t.TypeName, allowNull: false).Length(25);
            setter.Map(t => t.CategoryCode, ColumnAction.Insert).Length(25);
            setter.Map(t => t.Remark).Length(200);
            setter.Map(t => t.SortedTime, ColumnAction.EditAndDefault);
            setter.Map(t => t.CreatedTime, ColumnAction.Default);
        }
    }
}
