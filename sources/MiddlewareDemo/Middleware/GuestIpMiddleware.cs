using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MiddlewareDemo.Middleware
{
    public class GuestIpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GuestIpMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<GuestIpMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogWarning("User IP: " + context.Connection.RemoteIpAddress);
            await _next.Invoke(context);
        }
    }


    public static class GuestIpMiddlewareExt
    {
        public static IApplicationBuilder UseGuestIp(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GuestIpMiddleware>();
        }
    }
}