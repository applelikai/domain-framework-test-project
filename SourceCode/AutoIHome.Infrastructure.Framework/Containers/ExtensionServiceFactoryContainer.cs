using Domain.Framework.Core;
using Domain.Framework.Core.Factories;
using System;

namespace AutoIHome.Infrastructure.Framework.Containers
{
    /// <summary>
    /// 第三方业务工厂容器
    /// </summary>
    internal class ExtensionServiceFactoryContainer : ContainerBase<IServiceFactory>
    {
        /// <summary>
        /// 创建使用第三方技术的业务工厂
        /// </summary>
        /// <param name="extensionkillName">第三方技术名称</param>
        /// <returns>业务工厂</returns>
        protected override IServiceFactory Create(string extensionkillName)
        {
            //获取业务工厂类型
            string typeName = string.Format("Extension.{0}.Framework.ServiceFactory, Extension.{0}", extensionkillName);
            Type type = Type.GetType(typeName);
            if (type == null)
                return null;
            //获取业务工厂对象
            dynamic serviceFactory = Activator.CreateInstance(type);
            return serviceFactory;
        }
    }
}
