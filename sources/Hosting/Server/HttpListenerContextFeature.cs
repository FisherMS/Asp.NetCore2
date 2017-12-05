using System;
using System.IO;
using System.Net;

namespace Hosting
{
    public class HttpListenerContextFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        public HttpListenerContextFeature(HttpListenerContext context)
        {
            Context = context;
            OutputStream = context.Response.OutputStream;
            Url = context.Request.Url;
        }

        public HttpListenerContext Context { get; }

        public Uri Url { get; }

        public Stream OutputStream { get; }

        public long ContentLength64
        {
            get { return Context.Response.ContentLength64; }
            set { Context.Response.ContentLength64 = value; }
        }

        public string ContentType
        {
            get { return Context.Response.ContentType; }
            set { Context.Response.ContentType = value; }
        }

        public int StatusCode
        {
            get { return Context.Response.StatusCode; }
            set { Context.Response.StatusCode = value; }
        }
    }
}