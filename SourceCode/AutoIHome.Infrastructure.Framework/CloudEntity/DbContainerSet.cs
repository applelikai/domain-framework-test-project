using CloudEntity.Core.Data.Entity;
using CloudEntity.Data.Entity;
using CloudEntity.Mapping;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AutoIHome.Infrastructure.Framework.CloudEntity
{
    /// <summary>
    /// 数据容器集类
    /// </summary>
    public class DbContainerSet
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        private object _locker;
        /// <summary>
        /// 配置
        /// </summary>
        private IConfiguration _configuration;
        /// <summary>
        /// 数据容器字典(key:数据操作对象所在程序集名称, value:操作当前程序集对象的数据容器)
        /// </summary>
        private IDictionary<string, IDbContainer> _dbContainers;

        /// <summary>
        /// 获取Mapper容器
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>Mapper容器</returns>
        private IMapperContainer GetMapperContainer(string assemblyName)
        {
            //获取对象类型
            Type containerType = Type.GetType(string.Format("{0}.CloudEntity.Framework.MapperContainer, {0}.CloudEntity", assemblyName));
            //获取Mapper容器对象
            dynamic mapperContainer = Activator.CreateInstance(containerType);
            return mapperContainer;
        }
        /// <summary>
        /// 获取数据库初始化器
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="mapperContainer">Mapper容器</param>
        /// <returns>数据库初始化器</returns>
        private DbInitializer GetDbInitializer(string assemblyName, IMapperContainer mapperContainer)
        {
            //获取数据库初始化器的类型
            Type initializerType = Type.GetType(_configuration[$"DbInitializers:{assemblyName}"]);
            //获取数据库初始化器
            dynamic initializer = Activator.CreateInstance(initializerType, mapperContainer);
            return initializer;
        }

        /// <summary>
        /// 创建数据容器
        /// </summary>
        /// <param name="assemblyName">数据操作对象所在程序集名称</param>
        /// <returns>数据容器</returns>
        private IDbContainer CreateContainer(string assemblyName)
        {
            //获取连接字符串
            string connectionString = _configuration.GetConnectionString(assemblyName);
            //获取Mapper容器
            IMapperContainer mapperContainer = this.GetMapperContainer(assemblyName);
            //获取数据库初始化器
            DbInitializer initializer = this.GetDbInitializer(assemblyName, mapperContainer);
            //获取数据容器
            return DbContainer.Get(connectionString, initializer);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">配置</param>
        public DbContainerSet(IConfiguration configuration)
        {
            _locker = new object();
            _configuration = configuration;
            _dbContainers = new Dictionary<string, IDbContainer>();
        }
        /// <summary>
        /// 获取数据容器
        /// </summary>
        /// <param name="assemblyName">数据操作对象所在程序集名称</param>
        /// <returns>数据容器</returns>
        public IDbContainer Get(string assemblyName)
        {
            Start:
            //若字典中包含当前数据容器,则直接获取
            if (_dbContainers.ContainsKey(assemblyName))
                return _dbContainers[assemblyName];
            //进入线程锁
            lock (_locker)
            {
                //若字典中不包含当前数据容器,则添加
                if (!_dbContainers.ContainsKey(assemblyName))
                    _dbContainers.Add(assemblyName, this.CreateContainer(assemblyName));
                //回到Start
                goto Start;
            }
        }
    }
}
