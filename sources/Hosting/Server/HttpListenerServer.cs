using System.Net;

namespace Hosting
{
    public class HttpListenerServer : IServer
    {
        public HttpListenerServer(string url)
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add(url ?? "http://localhost:3721/");
        }

        public HttpListener Listener { get; }

        public IFeatureCollection Features { get; } = new FeatureCollection();

        public void Start<TContext>(IHttpApplication<TContext> application)
        {
            Listener.Start();
            while (true)
            {
                var context = Listener.GetContext();

                var feature = new HttpListenerContextFeature(context);
                var contextFeatures = new FeatureCollection();
                contextFeatures.Set<IHttpRequestFeature>(feature);
                contextFeatures.Set<IHttpResponseFeature>(feature);

                application.ProcessRequestAsync(
                        application.CreateContext(contextFeatures))
                    .ContinueWith(_ => context.Response.Close());
            }
        }
    }
}