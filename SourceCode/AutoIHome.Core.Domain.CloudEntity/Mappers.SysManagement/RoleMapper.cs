using AutoIHome.Core.Domain.CloudEntity.Framework;
using AutoIHome.Core.Domain.Entities.SysManagement;
using CloudEntity;
using CloudEntity.Mapping;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.SysManagement
{
    /// <summary>
    /// 角色的Mapper类
    /// </summary>
    internal class RoleMapper : BaseMapper<Role>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Role> setter)
        {
            setter.Map(r => r.RoleId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(r => r.RoleName, allowNull: false).DataType(DataType.Nvarchar, 25);
            setter.Map(r => r.Remark).DataType(DataType.Text);
            setter.Map(r => r.CreatedTime, ColumnAction.Default);
        }
    }
}
