using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp4NetCoreHttp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("settings.json");//覆盖默认的配置
                    config.AddCommandLine(args);//读取命令行参数
                })
                .UseStartup<Startup>()
                .Build();
    }
}
