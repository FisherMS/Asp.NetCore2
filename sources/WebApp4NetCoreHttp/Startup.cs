using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp4NetCoreHttp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration configuration,IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            //applicationLifetime 就是以前的Global里的相关事件
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("ApplicationStarted");
            });

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("ApplicationStopping");
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("ApplicationStopped");
            });





            app.Run(async (context) =>
            {
                

                
                await context.Response.WriteAsync($"IsStaging:{env.IsStaging()};\r\nIsProduction:{env.IsProduction()}\r\nIsDevelopment:{env.IsDevelopment()};\r\nWebRootPath:{env.WebRootPath};\r\nWebRootFileProvider:{env.WebRootFileProvider};\r\nEnvironmentName:{env.EnvironmentName};\r\nContentRootPath:{env.ContentRootPath};\r\nApplicationName:{env.ApplicationName};\r\nContentRootFileProvider:{env.ContentRootFileProvider};name={configuration["name"]}\r\n{configuration["ConnectionStrings:DefaultConnection"]}");

                //await context.Response.WriteAsync("Hello World!");
            });
        }


    }
}
