using AutoIHome.Core.Domain.Entities.EmpManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.EmpManagement
{
    /// <summary>
    /// 员工的Mapper类
    /// </summary>
    internal class EmployeeMapper : TableMapper<Employee>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Emp_Employees");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Employee> setter)
        {
            setter.Map(e => e.EmployeeNo, ColumnAction.PrimaryAndInsert).Length(25);
            setter.Map(e => e.EmployeeName, allowNull: false).Length(25);
            setter.Map(e => e.Sex, allowNull: false).Length(2);
            setter.Map(e => e.IdNumber, allowNull: false).Length(18);
            setter.Map(e => e.Birthday, allowNull: false);
            setter.Map(e => e.DepartmentNo, allowNull: false).Length(25);
            setter.Map(e => e.JobId, allowNull: false).Length(36);
            setter.Map(e => e.PhoneNumber).Length(25);
            setter.Map(e => e.Email).Length(25);
            setter.Map(e => e.Address).Length(100);
            setter.Map(e => e.JoinDate);
            setter.Map(e => e.LeaveDate);
            setter.Map(e => e.CreatedTime, ColumnAction.Default);
            setter.Map(e => e.IsDeleted, ColumnAction.Default);
        }
    }
}
