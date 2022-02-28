using AutoIHome.Core.Domain.CloudEntity.Framework;
using AutoIHome.Core.Domain.Entities.EmpManagement;
using CloudEntity;
using CloudEntity.Mapping;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.EmpManagement
{
    /// <summary>
    /// 部门的Mapper类
    /// </summary>
    internal class DepartmentMapper : BaseMapper<Department>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Department> setter)
        {
            setter.Map(d => d.DepartmentId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(d => d.DepartmentName, allowNull: false).Length(25);
            setter.Map(d => d.DepartmentManager).Length(25);
            setter.Map(d => d.ParentId).Length(36);
            setter.Map(d => d.Remark).DataType(DataType.Text);
            setter.Map(d => d.CreatedTime, ColumnAction.Default);
        }
    }
}
