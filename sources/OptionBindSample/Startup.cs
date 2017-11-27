using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionBindSample
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {

                //  Bind读取默认配置appsettings.json到C#实例 

                var cls = new Class();
                Configuration.Bind(cls);

                //await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync($"{cls.ClassNo}");
                await context.Response.WriteAsync($"{cls.ClassDesc}");

                await context.Response.WriteAsync($"{cls.Students.Count}");
               
                //await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
