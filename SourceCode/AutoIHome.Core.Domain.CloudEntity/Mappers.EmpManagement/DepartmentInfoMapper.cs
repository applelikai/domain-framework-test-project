using AutoIHome.Core.Domain.CloudEntity.Framework;
using AutoIHome.Core.Domain.Entities.EmpManagement;
using CloudEntity.Mapping;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.EmpManagement
{
    /// <summary>
    /// 部门简要信息的Mapper类
    /// </summary>
    internal class DepartmentInfoMapper : BaseMapper<DepartmentInfo>
    {
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<DepartmentInfo> setter)
        {
            setter.Map(d => d.DepartmentInfoId, ColumnAction.PrimaryAndInsert);
        }
    }
}
