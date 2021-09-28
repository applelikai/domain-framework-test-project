using CloudEntity.Mapping;
using CloudEntity.Mapping.Common;
using System;
using System.Reflection;
using System.Text;

namespace Base.RegManagement.Domain.CloudEntity.Framework
{
    /// <summary>
    /// Mapper容器
    /// </summary>
    public class MapperContainer : MapperContainerBase
    {
        /// <summary>
        /// 创建Table元数据解析器
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>Table元数据解析器</returns>
        protected override ITableMapper CreateTableMapper(Type entityType)
        {
            //获取当前程序集名称
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //获取Mapper类型命名空间
            StringBuilder namespaceBuilder = new StringBuilder(entityType.Namespace);
            namespaceBuilder.Replace(entityType.Assembly.GetName().Name, assemblyName);
            namespaceBuilder.Replace("Entities", "Mappers");
            //Mapper类型全名
            string targetMapperTypeName = string.Format("{0}.{1}Mapper", namespaceBuilder.ToString(), entityType.Name);
            //创建Mapper对象
            return Activator.CreateInstance(Type.GetType(targetMapperTypeName)) as ITableMapper;
        }
    }
}
