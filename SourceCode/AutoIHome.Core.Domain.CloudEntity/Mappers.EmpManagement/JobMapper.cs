using AutoIHome.Core.Domain.CloudEntity.Framework;
using AutoIHome.Core.Domain.Entities.EmpManagement;
using CloudEntity;
using CloudEntity.Mapping;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.EmpManagement
{
    /// <summary>
    /// 职位的Mapper类
    /// </summary>
    internal class JobMapper : BaseMapper<Job>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Job> setter)
        {
            setter.Map(j => j.JobId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(j => j.JobName, allowNull: false).DataType(DataType.Nvarchar, 25);
            setter.Map(j => j.DepartmentId, ColumnAction.Insert).Length(36);
            setter.Map(j => j.Remark).DataType(DataType.Text);
            setter.Map(j => j.CreatedTime, ColumnAction.Default);
        }
    }
}
