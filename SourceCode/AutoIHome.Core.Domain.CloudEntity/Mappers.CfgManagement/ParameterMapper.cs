using AutoIHome.Core.Domain.Entities.CfgManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.CfgManagement
{
    /// <summary>
    /// 参数的Mapper类
    /// </summary>
    internal class ParameterMapper : TableMapper<Parameter>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Cfg_Parameters");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<Parameter> setter)
        {
            setter.Map(p => p.ParameterName, ColumnAction.PrimaryAndInsert).Length(50);
            setter.Map(p => p.ParameterValue, allowNull: false).Length(100);
            setter.Map(p => p.CategoryId, ColumnAction.Insert).Length(36);
            setter.Map(p => p.Remark).Length(200);
            setter.Map(t => t.SortedTime, ColumnAction.EditAndDefault);
            setter.Map(p => p.CreatedTime, ColumnAction.Default);
        }
    }
}
