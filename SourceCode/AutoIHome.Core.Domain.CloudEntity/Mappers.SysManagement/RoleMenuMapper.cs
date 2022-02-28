using AutoIHome.Core.Domain.CloudEntity.Framework;
using AutoIHome.Core.Domain.Entities.SysManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.SysManagement
{
    /// <summary>
    /// 角色菜单的Mapper类
    /// </summary>
    internal class RoleMenuMapper : BaseMapper<RoleMenu>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<RoleMenu> setter)
        {
            setter.Map(m => m.MenuId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(m => m.MenuName, ColumnAction.Insert).Length(50);
            setter.Map(m => m.MenuType, ColumnAction.Insert).Length(25);
            setter.Map(m => m.RoleId, ColumnAction.Insert).Length(36);
            setter.Map(m => m.CreatedTime, ColumnAction.Default);
        }
    }
}
