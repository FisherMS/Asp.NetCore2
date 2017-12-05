using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hosting
{
    public class WebHost : IWebHost
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Type _startupType;

        public WebHost(IServiceCollection services, Type startupType)
        {
            _serviceProvider = services.BuildServiceProvider();
            _startupType = startupType;
        }

        public void Start()
        {
            var appBuilder =
                _serviceProvider.GetRequiredService<IApplicationBuilderFactory>()
                    .CreateBuilder();
            _serviceProvider.GetRequiredService<IStartupLoader>()
                .GetConfigureDelegate(_startupType)(appBuilder);
            var server = _serviceProvider.GetRequiredService<IServerFactory>()
                .CreateServer();
            server.Start(new HostingApplication(appBuilder.Build()));
        }
    }
}