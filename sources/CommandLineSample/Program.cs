using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CommandLineSample
{
    class Program
    {
        static void Main(string[] args)
        {

            //默认的初始化参数
            var settings = new Dictionary<string, string>(){{"name","atwind"},{"age","99"}};



            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(settings) //从内存中加入
                .AddCommandLine(args) //从命令行的参数中加入
                ;

            var configuration = builder.Build();

            Console.WriteLine($"name:{configuration["name"]}");
            Console.WriteLine($"age:{configuration["age"]}");





            //Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
