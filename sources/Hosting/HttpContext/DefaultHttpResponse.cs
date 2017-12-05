using System.IO;

namespace Hosting
{
    public class DefaultHttpResponse : HttpResponse
    {
        public DefaultHttpResponse(DefaultHttpContext context)
        {
            ResponseFeature = context.HttpContextFeatures.Get<IHttpResponseFeature>();
        }

        public override Stream OutputStream
        {
            get { return ResponseFeature.OutputStream; }
        }

        public override string ContentType
        {
            get { return ResponseFeature.ContentType; }
            set { ResponseFeature.ContentType = value; }
        }

        public override int StatusCode
        {
            get { return ResponseFeature.StatusCode; }
            set { ResponseFeature.StatusCode = value; }
        }

        public IHttpResponseFeature ResponseFeature { get; }
    }
}