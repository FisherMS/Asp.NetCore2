using System;

namespace Hosting
{
    public class StartupLoader : IStartupLoader
    {
        public Action<IApplicationBuilder> GetConfigureDelegate(Type startupType)
        {
            return
                app =>
                    startupType.GetMethod("Configure").Invoke(Activator.CreateInstance(startupType), new object[] {app});
        }
    }
}