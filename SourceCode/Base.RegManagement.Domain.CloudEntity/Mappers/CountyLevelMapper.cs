using Base.RegManagement.Domain.CloudEntity.Framework;
using Base.RegManagement.Domain.Entities;
using CloudEntity;
using CloudEntity.Mapping;

namespace Base.RegManagement.Domain.CloudEntity.Mappers
{
    /// <summary>
    /// 县级行政区的Mapper类
    /// </summary>
    internal class CountyLevelMapper : BaseMapper<CountyLevel>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<CountyLevel> setter)
        {
            setter.Map(c => c.CountyCode, ColumnAction.PrimaryAndInsert).Length(25);
            setter.Map(c => c.CountyName, allowNull: false).Length(25);
            setter.Map(c => c.CountyType, allowNull: false).Length(25);
            setter.Map(c => c.ProvinceCode, ColumnAction.Insert).Length(25);
            setter.Map(c => c.PrefectureCode, ColumnAction.Insert, allowNull: true).Length(25);
            setter.Map(c => c.PostalCode, allowNull: false).DataType(DataType.Char, 6);
            setter.Map(c => c.Remark).DataType(DataType.Text);
            setter.Map(c => c.CreatedTime, ColumnAction.Default);
        }
    }
}
