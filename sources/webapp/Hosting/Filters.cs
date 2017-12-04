using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace webapp.Hosting
{
    public class A : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            Console.WriteLine("This is A Start!");
            return app =>
            {
                Console.WriteLine("This is A Next!");
                next(app);
            };
        }
    }

    public class B : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            Console.WriteLine("This is B Start!");
            return app =>
            {
                Console.WriteLine("This is B Next!");
                next(app);
            };
        }
    }



}