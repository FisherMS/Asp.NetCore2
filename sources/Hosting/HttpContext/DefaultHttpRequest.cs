using System;

namespace Hosting
{
    public class DefaultHttpRequest : HttpRequest
    {
        public DefaultHttpRequest(DefaultHttpContext context)
        {
            RequestFeature = context.HttpContextFeatures.Get<IHttpRequestFeature>();
        }

        public IHttpRequestFeature RequestFeature { get; }

        public override Uri Url
        {
            get { return RequestFeature.Url; }
        }
    }
}