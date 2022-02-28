using AutoIHome.Core.Domain.Entities.EmpManagement;
using AutoIHome.Core.Domain.Entities.SysManagement;
using AutoIHome.Infrastructure.Framework.CloudEntity;
using Base.RegManagement.Domain.Entities;
using CloudEntity.Data.Entity;
using Microsoft.Extensions.Configuration;
using System;

namespace AutoIHome.Platform.Test
{
    /// <summary>
    /// 起始程序
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 数据容器集
        /// </summary>
        private static DbContainerSet _containerSet;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static Program()
        {
            //获取配置
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            //初始化数据容器集
            _containerSet = new DbContainerSet(configuration);
        }
        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="args">控制台参数</param>
        private static void Main(string[] args)
        {
            //获取数据容器
            IDbContainer container = _containerSet.Get("AutoIHome.Core.Domain");
            //其他操作
            container.InitTable<RoleMenu>();
        }
    }
}
