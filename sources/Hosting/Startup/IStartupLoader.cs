using System;

namespace Hosting
{
    public interface IStartupLoader
    {
        Action<IApplicationBuilder> GetConfigureDelegate(Type startupType);
    }
}