using CloudEntity;
using CloudEntity.Core.Data.Entity;
using CloudEntity.Data;
using CloudEntity.Mapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoIHome.Infrastructure.CloudEntity.MySqlClient
{
    /// <summary>
    /// MySql Table初始化器
    /// </summary>
    internal class MySqlTableInitializer : TableInitializer
    {
        /// <summary>
        /// 判断当前table是否存在
        /// </summary>
        /// <param name="dbHelper">操作数据库的Helper</param>
        /// <param name="tableHeader">Table头</param>
        /// <returns>当前table是否存在</returns>
        public override bool IsExist(DbHelper dbHelper, ITableHeader tableHeader)
        {
            //获取sql参数数组
            IList<IDbDataParameter> parameters = new List<IDbDataParameter>
            {
                dbHelper.Parameter("TableName", tableHeader.TableName)
            };
            //初始化sql命令
            StringBuilder commandText = new StringBuilder();
            commandText.AppendLine("SELECT COUNT(*)");
            commandText.AppendLine("  FROM information_schema.Tables t");
            commandText.AppendLine(" WHERE t.Table_Name = @TableName");
            //若有架构名则带上架构名
            if (!string.IsNullOrEmpty(tableHeader.SchemaName))
            {
                commandText.AppendLine("   AND t.Table_Schema = @SchemaName");
                parameters.Add(dbHelper.Parameter("SchemaName", tableHeader.SchemaName));
            }
            //执行获取结果
            int result = TypeHelper.ConvertTo<int>(dbHelper.GetScalar(commandText.ToString(), parameters: parameters.ToArray()));
            return result > 0;
        }
    }
}
