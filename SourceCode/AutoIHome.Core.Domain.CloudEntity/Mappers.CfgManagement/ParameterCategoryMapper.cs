using AutoIHome.Core.Domain.Entities.CfgManagement;
using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;

namespace AutoIHome.Core.Domain.CloudEntity.Mappers.CfgManagement
{
    /// <summary>
    /// 参数分类的Mapper类
    /// </summary>
    internal class ParameterCategoryMapper : TableMapper<ParameterCategory>
    {
        /// <summary>
        /// 获取Table元数据
        /// </summary>
        /// <returns>Table元数据</returns>
        protected override ITableHeader GetHeader()
        {
            return base.GetHeader("Cfg_ParameterCategories");
        }
        /// <summary>
        /// 设置属性映射
        /// </summary>
        /// <param name="setter">属性映射设置器</param>
        protected override void SetColumnMappers(IColumnMapSetter<ParameterCategory> setter)
        {
            setter.Map(c => c.CategoryId, ColumnAction.PrimaryAndInsert);
        }
    }
}
