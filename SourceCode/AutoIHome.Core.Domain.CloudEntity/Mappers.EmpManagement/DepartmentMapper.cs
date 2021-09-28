using AutoIHome.Core.Domain.Entities.EmpManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.EmpManagement
{
    /// <summary>
    /// 部门的Mapper类
    /// </summary>
    internal class DepartmentMapper : TableMapper<Department>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Emp_Departments");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Department> setter)
        {
            setter.Map(d => d.DepartmentNo, ColumnAction.PrimaryAndInsert).Length(25);
            setter.Map(d => d.DepartmentName, allowNull: false).Length(25);
            setter.Map(d => d.DepartmentManager, allowNull: false).Length(25);
            setter.Map(d => d.ParentDepartmentNo).Length(25);
            setter.Map(d => d.Remark).Length(100);
            setter.Map(d => d.CreatedTime, ColumnAction.Default);
        }
    }
}
