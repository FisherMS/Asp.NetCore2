using System;

namespace Hosting
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseStartup(Type startupType);
        IWebHostBuilder UseServer(IServerFactory factory);
        IWebHost Build();
    }
}
