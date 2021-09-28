using Base.RegManagement.Domain.CloudEntity.Framework;
using Base.RegManagement.Domain.Entities;
using CloudEntity;
using CloudEntity.Mapping;

namespace Base.RegManagement.Domain.CloudEntity.Mappers
{
    /// <summary>
    /// 省级行政区的Mapper类
    /// </summary>
    internal class ProvinceLevelMapper : BaseMapper<ProvinceLevel>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<ProvinceLevel> setter)
        {
            setter.Map(p => p.ProvinceCode, ColumnAction.PrimaryAndInsert).Length(25);
            setter.Map(p => p.ShortName, allowNull: false).DataType(DataType.Char, 2);
            setter.Map(p => p.ProvinceName, allowNull: false).Length(25);
            setter.Map(p => p.ProvinceType, allowNull: false).Length(25);
            setter.Map(p => p.ProvinceEnName).Length(50);
            setter.Map(p => p.AreaCode).DataType(DataType.Char, 4);
            setter.Map(p => p.LicensePlateCode).Length(10);
            setter.Map(p => p.Remark).DataType(DataType.Text);
            setter.Map(p => p.CreatedTime, ColumnAction.Default);
        }
    }
}
