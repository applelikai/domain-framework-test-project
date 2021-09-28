using AutoIHome.Infrastructure.Framework.CloudEntity;
using AutoIHome.Infrastructure.Framework.Factories;
using Bootstrap.AspNetCore.TagHelpers;
using Domain.Framework.Core.Factories;
using Domain.Framework.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoIHome.Platform.Web
{
    /// <summary>
    /// 起始类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //添加MVC
            services.AddControllersWithViews().AddJsonOptions(option =>
            {
                //数据格式首字母小写
                //options.JsonSerializerOptions.PropertyNamingPolicy =JsonNamingPolicy.CamelCase;
                //JsonNamingPolicy.CamelCase首字母小写（默认）,null则为不改变大小写
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //配置跨域处理，允许所有来源
            services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            //BootStrap的TagHelper样式配置
            StyleConfigure.ButtonMode = ButtonMode.Outline;
            StyleConfigure.InvalidMessageMode = InvalidMessageMode.Feedback;
        }
        /// <summary>
        /// configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="env">web host environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用https
            app.UseHttpsRedirection();
            //启用静态文件
            app.UseStaticFiles();
            //启用路由
            app.UseRouting();
            //添加路由配置
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Login}/{id?}");
                endpoints.MapAreaControllerRoute("areas", "areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
            //获取配置
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            //领域框架依赖注入配置
            DbContainerSet dbContainerSet = new DbContainerSet(configuration);
            ImplementContainer.Add<FactoryContainerBase>(new FactoryContainer(dbContainerSet));
        }
    }
}
