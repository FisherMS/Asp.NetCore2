using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hosting
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private Type _startupType;
        private IServiceCollection _services;

        public WebHostBuilder()
        {
            _services = new ServiceCollection()
                .AddTransient<IStartupLoader, StartupLoader>()
                .AddTransient<IApplicationBuilderFactory, ApplicationBuilderFactory>();
        }

        public IWebHostBuilder UseStartup(Type startupType)
        {
            _startupType = startupType;
            return this;
        }

        public IWebHostBuilder UseServer(IServerFactory factory)
        {
            _services.AddSingleton<IServerFactory>(factory);
            return this;
        }

        public IWebHost Build()
        {
            return new WebHost(_services, _startupType);
        }
    }
}