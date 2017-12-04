using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using webapp.Hosting;
using webapp.Middleware;

namespace webapp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IMyService, MyServiceImplement>(); //IOC注册服务


            //Note:WebHost 在执行 Starup 类中Configure方法之前，会从 DI 系统中获取所有的IStartupFilter来执行
            services.AddSingleton<IStartupFilter, A>();
            services.AddSingleton<IStartupFilter, B>();


            //如上，我们定义了一个在台后每5秒刷新一次缓存的服务，并在 ASP.NET Core 程序停止时，优雅的关闭。最后，将它注册到 DI 系统中即可
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IHostedService, CacheHostService>();
            //WebHost 在启动 HTTP Server 之后，会从 DI 系统中获取所有的IHostedService，来启动我们注册的 HostedService






        }

        //Note:Configure  only accecpt one.

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.Run(async (context) =>
        //    {
        //        await context.Response.WriteAsync("Hello World!");
        //    });
        //}



        //public void Configure(IApplicationBuilder app)
        //{
        //    Console.WriteLine("This is Configure!");

        //    //注册了一个最简单的中间件,通过浏览器访问，便可以看到 “Hello ASP.NET Core!” 
        //    app.Use(next =>
        //    {
        //        return async (context) =>
        //        {
        //            await context.Response.WriteAsync("Hello ASP.NET Core!");
        //        };
        //    });
        //}


        //public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
        //{
        //    appLifetime.ApplicationStarted.Register(() => Console.WriteLine("Started"));
        //    appLifetime.ApplicationStopping.Register(() => Console.WriteLine("Stopping"));
        //    appLifetime.ApplicationStopped.Register(() =>
        //    {
        //        Console.WriteLine("Stopped");
        //        Console.ReadKey();
        //    });

        //    appLifetime.ApplicationStarted.Register(() => Console.WriteLine("Started again.")); //注册的方法都会执行，最后的方法最先执行。 


        //    app.Use(next =>
        //    {
        //        return async (context) =>
        //        {
        //            await context.Response.WriteAsync("Hello ASP.NET Core!");
        //            appLifetime.StopApplication();
        //        };
        //    });
        //}


       // 我们使用Use注册两个简单的中间件：
        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
        {
            //当访问//assets下的资源时后续中间件全部放弃。
            app.MapWhen(context => context.Request.Path.StartsWithSegments("/assets"),
                appBuilder => appBuilder.UseStaticFiles());


            app.UseGuestIp();//使用中间件


            //UseWhen 示例。
            //UseWhen是非常强大和有用的，建议当我们想要针对某些请求做一些特定的处理时，我们应该只为这些请求注册特定的中间件，而不是在中间件中去判断请求是否符合预期来选择执行某些操作，这样能有更好的性能。
            //当路径以/api开头时再调用一次UseGuestIp
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
            {
                appBuilder.UseGuestIp();
            });

            ////当符合条件后执行当前及以前的中间件，后续的不再执行。反之则当前的中间件不执行。其它的都执行。（就是路由走了，原来的线路分支去了。）
            //app.MapWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
            //{
            //    appBuilder.UseGuestIp();
            //});



            //而B的执行会嵌套在A的里面，因此A是第一个处理Request，并且最后一个收到Respone，这样就构成一个经典的的U型管道。
            app.Use(next =>
            {
                Console.WriteLine("A");// 这个内容在管道初始化的时候执行，后面的每次请求就不执行了。

                return async (context) =>
                {
                    // 1. 对Request做一些处理
                    // TODO

                    // 2. 调用下一个中间件
                    Console.WriteLine("A-BeginNext");
                    await next(context);
                    //await context.Response.WriteAsync("A-BeginNext"); //会覆盖到上一个中间件B的返回内容。
                    Console.WriteLine("A-EndNext");

                    // 3. 生成 Response
                    //TODO
                };
            });
            app.Use(next =>
            {
                Console.WriteLine("B");// 这个内容在管道初始化的时候执行，后面的每次请求就不执行了。

                return async (context) =>
                {
                    // 1. 对Request做一些处理
                    // TODO

                    // 2. 调用下一个中间件
                    Console.WriteLine("B-BeginNext");
                    //await context.Response.WriteAsync("B-BeginNext"); //这个会截断后续的中间件。因为没有调用
                    await next(context);
                    Console.WriteLine("B-EndNext");

                    // 3. 生成 Response
                    //TODO
                };
            });

            //因为是Run，所以会截断后续的中间件。
            app.Run(async context =>
            {

                await context.Response.WriteAsync("进入第3个委托\r\n");
                await context.Response.WriteAsync("Hello from 3rd delegate.\r\n");
                await context.Response.WriteAsync("结束第3个委托\r\n");
            });
            app.Run(async context =>
            {

                await context.Response.WriteAsync("进入第4个委托\r\n");
                await context.Response.WriteAsync("Hello from 4th delegate.\r\n");
                await context.Response.WriteAsync("结束第4个委托\r\n");

            });

            //app.Use(next =>
            //{
            //    return async (context) =>
            //    {
            //        await context.Response.WriteAsync("Hello ASP.NET Core!");
            //        appLifetime.StopApplication();
            //    };
            //});

        }






        //然后在 Startup 中调用这些扩展方法：

        //public void ConfigureDevelopmentServices(IServiceCollection services)
        //{
        //    services.AddEF(Configuration.GetConnectionString("DefaultConnection");
        //}

        //public void ConfigureDevelopment(IApplicationBuilder app)
        //{
        //    services.UseEF();
        //}
    }



    //而在一个多层项目中，Sartup类一般是放在展现层中，我们在其它层也需要注册一些服务或者配置请求管道时，通常会写一个扩展方法
    //public static class EfRepositoryExtensions
    //{
    //    public static void AddEF(this IServiceCollection services, string connectionStringName)
    //    {
    //        services.AddDbContext<AppDbContext>(options =>
    //                options.UseSqlServer(connectionStringName), opt => opt.EnableRetryOnFailure())
    //            );

    //        services.TryAddScoped<IDbContext, AppDbContext>();
    //        services.TryAddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));

    //            ...
    //    }

    //    public static void UseEF(IApplicationBuilder app)
    //    {
    //        app.UseIdentity();
    //    }
    //}


    //感觉这种方式非常丑陋，而在上一章中，我们知道 WebHost 会在 Starup 这前调用 IHostingStartup，于是我们便以如下方式来实现：
    //[assembly: HostingStartup(typeof(Zero.EntityFramework.EFRepositoryStartup))]
    //namespace Zero.EntityFramework
    //{
    //    public class EFRepositoryStartup : IHostingStartup
    //    {
    //        public void Configure(IWebHostBuilder builder)
    //        {
    //            builder.ConfigureServices(services =>
    //            {
    //                services.AddDbContext<AppDbContext>(options =>
    //                        options.UseSqlServer(connectionStringName), opt => opt.EnableRetryOnFailure())

    //                    );

    //                services.TryAddScoped<IDbContext, AppDbContext>();
    //                services.TryAddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));

    //                    ...
    //            }); 

    //            builder.Configure(app => {
    //                app.UseIdentity();
    //            });
    //        }
    //    }
    //}

    //如上，只需实现 IHostingStartup 接口，要清爽简单的多，怎一个爽字了得！不过，还需要进行注册才会被WebHost执行，首先要指定HostingStartupAttribute程序集特性，其次需要配置 WebHost 中的 IConfiguration[hostingStartupAssemblies]，以便 WebHost 能找到我们的程序集，可以使用如下方式配置：
    //WebHost.CreateDefaultBuilder(args)
    //// 如需指定多个程序集时，使用 ; 分割
    //.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "Zero.Application;Zero.EntityFramework")

    //这样便完成了 IHostingStartup 注册，不过还需要将包含IHostingStartup的程序集放到 Bin 目录下，否则根本无法加载。不过 ASP.NET Core 也提供了类似插件的方式来指定IHostingStartup程序集的查找位置，可通过设置DOTNET_ADDITIONAL_DEPS和ASPNETCORE_HOSTINGSTARTUPASSEMBLIES来实现，而这里就不再多说。




}
