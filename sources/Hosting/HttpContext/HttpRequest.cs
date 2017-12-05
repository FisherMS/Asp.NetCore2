using System;

namespace Hosting
{
    public abstract class HttpRequest
    {
        public abstract Uri Url { get; }
    }
}
