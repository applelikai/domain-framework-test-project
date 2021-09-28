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
    /// MySql列初始化器
    /// </summary>
    internal class MySqlColumnInitializer : ColumnInitializer
    {
        /// <summary>
        /// 查询获取当前表中所有的列
        /// </summary>
        /// <param name="dbHelper">操作数据库的DbHelper</param>
        /// <param name="tableHeader">table头</param>
        /// <returns>当前表中所有的列</returns>
        protected override IEnumerable<string> GetColumns(DbHelper dbHelper, ITableHeader tableHeader)
        {
            //初始化sql参数数组
            IList<IDbDataParameter> parameters = new List<IDbDataParameter>
            {
                dbHelper.Parameter("TableName", tableHeader.TableName)
            };
            //初始化sql
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("SELECT c.COLUMN_NAME");
            sqlBuilder.AppendLine("  FROM information_schema.COLUMNS c");
            sqlBuilder.AppendLine(" WHERE c.TABLE_NAME = @TableName");
            //若有TABLE_SCHEMA查询条件则带上
            if (!string.IsNullOrEmpty(tableHeader.SchemaName))
            {
                sqlBuilder.AppendLine("   AND c.TABLE_SCHEMA = @SchemaName");
                parameters.Add(dbHelper.Parameter("SchemaName", tableHeader.SchemaName));
            }
            //执行查询获取所有列
            return dbHelper.GetResults(reader => reader.GetString(0), sqlBuilder.ToString(), parameters: parameters.ToArray());
        }
    }
}
