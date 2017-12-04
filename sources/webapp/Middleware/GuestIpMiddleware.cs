using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace webapp.Middleware
{


    /// <summary>
    /// 通常我们并不会去实现 IMiddleware 接口，而是采用基于约定的，更加灵活的方式来定义中间件，而此时，UseMiddleware 方法会通过反射来创建中间件的实例
    /// <remarks>
    /// 为 IMiddleware 实例的创建是直接从 DI 容器中来获取的，也就是说，如果我们没有将我们实现了 IMiddleware 接口的中间件注册到DI中，而直接使用 UseMiddleware 来注册时，会报错。
    /// </remarks>
    /// </summary>
    public class GuestIpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        //需要注意的是，Next 委托必须放在构造函数中，而不能放在 InvokeAsync 方法参数中，这是因为 Next 并不在DI系统中，而 ActivatorUtilities.CreateInstance 创建实例时，也会检查构造中是否具有 RequestDelegate 类型的 Next 参数，如果没有，则会抛出一个异常：“A suitable constructor for type '{instanceType}' could not be located. Ensure the type is concrete and services are registered for all parameters of a public constructor.”。


        public GuestIpMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<GuestIpMiddleware>();
        }

        //必须要有一个 Invoke 或 InvokeAsync 方法，两者也只能存在一个。
        //返回类型必须是 Task 或者继承自 Task。
        //Invoke 或 InvokeAsync 方法必须要有一个 HttpContext　类型的参数。

        public Task Invoke(HttpContext context)
        {
            _logger.LogWarning("User1 IP: " + context.Connection.RemoteIpAddress);
            return _next.Invoke(context);
        }

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    _logger.LogWarning("User2 IP: " + context.Connection.RemoteIpAddress);
        //    await _next.Invoke(context);
        //}
    }


    public static class GuestIpMiddlewareExt
    {
        public static IApplicationBuilder UseGuestIp(this IApplicationBuilder builder)
        {
            //UseMiddleware 方法会通过反射来创建中间件的实例
            return builder.UseMiddleware<GuestIpMiddleware>();
        }
    }
}