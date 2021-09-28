using AutoIHome.Core.Domain.Entities.SysManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.SysManagement
{
    /// <summary>
    /// 用户的Mapper类
    /// </summary>
    internal class UserMapper : TableMapper<User>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Sys_Users", tableAlias: "u");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<User> setter)
        {
            setter.Map(u => u.UserId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(u => u.UserName, allowNull: false).Length(25);
            setter.Map(u => u.Password, ColumnAction.Insert).Length(50);
            setter.Map(u => u.RoleId, allowNull: false).Length(36);
            setter.Map(u => u.EmployeeNo).Length(25);
            setter.Map(u => u.CreatedTime, ColumnAction.Default);
        }
    }
}
