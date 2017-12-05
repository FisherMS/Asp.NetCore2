namespace Hosting
{
    public class DefaultHttpContext : HttpContext
    {
        public DefaultHttpContext(IFeatureCollection httpContextFeatures)
        {
            HttpContextFeatures = httpContextFeatures;
            Request = new DefaultHttpRequest(this);
            Response = new DefaultHttpResponse(this);
        }

        public IFeatureCollection HttpContextFeatures { get; }

        public override HttpRequest Request { get; }
        public override HttpResponse Response { get; }
    }
}