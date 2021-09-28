using Base.RegManagement.Domain.CloudEntity.Framework;
using Base.RegManagement.Domain.Entities;
using CloudEntity;
using CloudEntity.Mapping;

namespace Base.RegManagement.Domain.CloudEntity.Mappers
{
    /// <summary>
    /// 地级行政区的Mapper类
    /// </summary>
    internal class PrefectureLevelMapper : BaseMapper<PrefectureLevel>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<PrefectureLevel> setter)
        {
            setter.Map(p => p.PrefectureCode, ColumnAction.PrimaryAndInsert).Length(25);
            setter.Map(p => p.PrefectureName, allowNull: false).Length(25);
            setter.Map(p => p.PrefectureType, allowNull: false).Length(25);
            setter.Map(p => p.PrefectureEnName).Length(50);
            setter.Map(p => p.ProvinceCode, ColumnAction.Insert).Length(25);
            setter.Map(p => p.AreaCode, allowNull: false).DataType(DataType.Char, 4);
            setter.Map(p => p.LicensePlateCode, allowNull: false).Length(10);
            setter.Map(p => p.Remark).DataType(DataType.Text);
            setter.Map(p => p.CreatedTime, ColumnAction.Default);
        }
    }
}
