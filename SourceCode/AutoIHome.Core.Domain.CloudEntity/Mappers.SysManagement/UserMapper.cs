using AutoIHome.Core.Domain.CloudEntity.Framework;
using AutoIHome.Core.Domain.Entities.SysManagement;
using CloudEntity.Mapping;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.SysManagement
{
    /// <summary>
    /// 用户的Mapper类
    /// </summary>
    internal class UserMapper : BaseMapper<User>
    {
        /// <summary>
        /// 获取表别名
        /// </summary>
        /// <returns>表别名</returns>
        protected override string GetTableAlias()
        {
            return "u";
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
            setter.Map(u => u.EmployeeId).Length(36);
            setter.Map(u => u.CreatedTime, ColumnAction.Default);
        }
    }
}
