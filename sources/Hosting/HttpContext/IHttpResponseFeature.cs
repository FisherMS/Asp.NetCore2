using System.IO;

namespace Hosting
{
    public interface IHttpResponseFeature
    {
        Stream OutputStream { get; }
        string ContentType { get; set; }
        int StatusCode { get; set; }
    }
}
