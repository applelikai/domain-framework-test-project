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
    /// ��ʼ��
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
            //���MVC
            services.AddControllersWithViews().AddJsonOptions(option =>
            {
                //���ݸ�ʽ����ĸСд
                //options.JsonSerializerOptions.PropertyNamingPolicy =JsonNamingPolicy.CamelCase;
                //JsonNamingPolicy.CamelCase����ĸСд��Ĭ�ϣ�,null��Ϊ���ı��Сд
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //���ÿ���������������Դ
            services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            //BootStrap��TagHelper��ʽ����
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
            //����https
            app.UseHttpsRedirection();
            //���þ�̬�ļ�
            app.UseStaticFiles();
            //����·��
            app.UseRouting();
            //���·������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Login}/{id?}");
                endpoints.MapAreaControllerRoute("areas", "areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
            //��ȡ����
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            //����������ע������
            DbContainerSet dbContainerSet = new DbContainerSet(configuration);
            ImplementContainer.Add<FactoryContainerBase>(new FactoryContainer(dbContainerSet));
        }
    }
}
