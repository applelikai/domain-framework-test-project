using MySql.Data.MySqlClient;
using CloudEntity.Data;
using System;
using System.Data;

namespace AutoIHome.Infrastructure.CloudEntity.MySqlClient
{
    /// <summary>
    /// 操作MySql的帮助类
    /// </summary>
    internal class MySqlHelper : DbHelper
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        protected override IDbConnection Connect(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
        /// <summary>
        /// 记录执行后的命令
        /// </summary>
        /// <param name="commandText">sql命令</param>
        protected override void RecordCommand(string commandText)
        {
            Console.WriteLine(commandText);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public MySqlHelper(string connectionString)
            : base(connectionString) { }
        /// <summary>
        /// 创建sql参数
        /// </summary>
        /// <returns>sql参数</returns>
        public override IDbDataParameter Parameter()
        {
            return new MySqlParameter();
        }
    }
}
