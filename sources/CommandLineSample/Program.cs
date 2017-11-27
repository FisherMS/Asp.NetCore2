using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace CommandLineSample
{
    class Program
    {
        static void Main(string[] args)
        {

            //读取Json配置文件夹的配置方法

            var builder = new ConfigurationBuilder()
                .AddJsonFile("cfg.json");
            var configuration = builder.Build();

            Console.WriteLine($"classNo:{configuration["classNo"]}");
            Console.WriteLine($"classDesc:{configuration["classDesc"]}");

            Console.WriteLine("Students:");

            Console.Write($"{configuration["students:0:name"]}");
            Console.WriteLine($"{configuration["students:0:age"]}");

            Console.Write($"{configuration["students:1:name"]}");
            Console.WriteLine($"{configuration["students:1:age"]}");

            Console.Write($"{configuration["students:2:name"]}");
            Console.WriteLine($"{configuration["students:2:age"]}");

            
            ////默认的初始化参数
            //var settings = new Dictionary<string, string>(){{"name","atwind"},{"age","99"}};

            //var builder = new ConfigurationBuilder()
            //    .AddInMemoryCollection(settings) //从内存中加入
            //    .AddCommandLine(args) //从命令行的参数中加入
            //    ;

            //var configuration = builder.Build();

            //Console.WriteLine($"name:{configuration["name"]}");
            //Console.WriteLine($"age:{configuration["age"]}");




            //Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
