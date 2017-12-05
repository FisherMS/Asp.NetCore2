using System.IO;

namespace Hosting
{
    public abstract class HttpResponse
    {
        public abstract Stream OutputStream { get; }
        public abstract string ContentType { get; set; }
        public abstract int StatusCode { get; set; }

        public void WriteFile(string fileName, string contentType)
        {
            if (File.Exists(fileName))
            {
                var content = File.ReadAllBytes(fileName);
                ContentType = contentType;
                OutputStream.Write(content, 0, content.Length);
            }
            StatusCode = 404;
        }
    }
}
