using System;
using System.Threading.Tasks;

namespace Hosting
{
    public interface IHttpApplication<TContext>
    {
        TContext CreateContext(IFeatureCollection contextFeatures);
        void DisposeContext(TContext context, Exception exception);
        Task ProcessRequestAsync(TContext context);
    }
}