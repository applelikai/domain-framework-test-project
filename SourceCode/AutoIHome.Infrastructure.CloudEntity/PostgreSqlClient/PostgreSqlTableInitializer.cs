using CloudEntity;
using CloudEntity.Core.Data.Entity;
using CloudEntity.Data;
using CloudEntity.Mapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoIHome.Infrastructure.CloudEntity.PostgreSqlClient
{
    /// <summary>
    /// PostgreSql Table初始化器
    /// </summary>
    internal class PostgreSqlTableInitializer : TableInitializer
    {
        /// <summary>
        /// 获取删除Table的命令
        /// </summary>
        /// <param name="tableHeader">Table元数据</param>
        /// <returns>删除Table的命令</returns>
        protected override string GetDropTableCommand(ITableHeader tableHeader)
        {
            //若架构名为空则直接(DROP TABLE 表名)
            if (string.IsNullOrEmpty(tableHeader.SchemaName))
                return $"DROP TABLE \"{tableHeader.TableName}\"";
            //若不为空则(DROP TABLE 架构名.表名)
            return $"DROP TABLE {tableHeader.SchemaName}.\"{tableHeader.TableName}\"";
        }
        /// <summary>
        /// 获取重命名Table的命令
        /// </summary>
        /// <param name="tableHeader">Table元数据</param>
        /// <param name="oldTableName">原来的Table名</param>
        /// <returns>重命名Table的命令</returns>
        protected override string GetRenameTableCommand(ITableHeader tableHeader, string oldTableName)
        {
            //若架构名为空则直接(ALTER TABLE 旧表名 RENAME TO 新表名)
            if (string.IsNullOrEmpty(tableHeader.SchemaName))
                return $"ALTER TABLE \"{oldTableName}\" RENAME TO \"{tableHeader.TableName}\"";
            //若不为空则(ALTER TABLE 架构名.旧表名 RENAME TO 架构名.新表名)
            return string.Format("ALTER TABLE {0}.\"{1}\" RENAME TO {0}.\"{2}\"", tableHeader.SchemaName, oldTableName, tableHeader.TableName);
        }

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
            commandText.AppendLine("  FROM information_schema.tables t");
            commandText.AppendLine(" WHERE t.table_name = @TableName");
            //若有架构名则带上架构名
            if (!string.IsNullOrEmpty(tableHeader.SchemaName))
            {
                commandText.AppendLine("   AND t.table_schema = @SchemaName");
                parameters.Add(dbHelper.Parameter("SchemaName", tableHeader.SchemaName));
            }
            //执行获取结果
            int result = TypeHelper.ConvertTo<int>(dbHelper.GetScalar(commandText.ToString(), parameters: parameters.ToArray()));
            return result > 0;
        }
    }
}
