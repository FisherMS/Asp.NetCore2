namespace Hosting
{
    public class HttpListenerServerFactory : IServerFactory
    {
        private readonly string _listenUrl;

        public HttpListenerServerFactory(string listenUrl = null)
        {
            _listenUrl = listenUrl ?? "http://localhost:3721/";
        }

        public IServer CreateServer()
        {
            return new HttpListenerServer(_listenUrl);
        }
    }
}