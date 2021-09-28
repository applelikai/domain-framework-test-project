using AutoIHome.Core.Domain.Entities.EmpManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.EmpManagement
{
    /// <summary>
    /// 职位的Mapper类
    /// </summary>
    internal class JobMapper : TableMapper<Job>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Emp_Jobs");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Job> setter)
        {
            setter.Map(j => j.JobId, ColumnAction.PrimaryAndInsert).Length(36);
            setter.Map(j => j.JobName, allowNull: false).Length(25);
            setter.Map(j => j.Remark).Length(100);
            setter.Map(j => j.CreatedTime, ColumnAction.Default);
        }
    }
}
