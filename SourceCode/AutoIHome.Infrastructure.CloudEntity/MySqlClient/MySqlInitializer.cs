using CloudEntity.CommandTrees;
using CloudEntity.CommandTrees.Commom.MySqlClient;
using CloudEntity.Core.Data.Entity;
using CloudEntity.Data;
using CloudEntity.Mapping;

namespace AutoIHome.Infrastructure.CloudEntity.MySqlClient
{
    /// <summary>
    /// 访问MySql数据库的初始化器
    /// </summary>
    public class MySqlInitializer : DbInitializer
    {
        /// <summary>
        /// Mapper容器
        /// </summary>
        private IMapperContainer _mapperContainer;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mapperContainer">Mapper容器</param>
        public MySqlInitializer(IMapperContainer mapperContainer)
        {
            _mapperContainer = mapperContainer;
        }
        /// <summary>
        /// 获取Column初始化器(Code First时需要使用)
        /// </summary>
        /// <returns>Column初始化器</returns>
        public override ColumnInitializer CreateColumnInitializer()
        {
            return new MySqlColumnInitializer();
        }
        /// <summary>
        /// 创建Table初始化器(Code First时需要使用)
        /// </summary>
        /// <returns>Table初始化器</returns>
        public override TableInitializer CreateTableInitializer()
        {
            return new MySqlTableInitializer();
        }
        /// <summary>
        /// 创建DbHelper对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>DbHelper对象</returns>
        public override DbHelper CreateDbHelper(string connectionString)
        {
            return new MySqlHelper(connectionString);
        }
        /// <summary>
        /// 获取创建sql命令生成树的工厂
        /// </summary>
        /// <returns>sql命令生成树的工厂</returns>
        public override ICommandTreeFactory CreateCommandTreeFactory()
        {
            return new MySqlCommandTreeFactory();
        }
        /// <summary>
        /// 创建Mapper容器
        /// </summary>
        /// <returns>Mapper容器</returns>
        public override IMapperContainer CreateMapperContainer()
        {
            return _mapperContainer;
        }
    }
}
