using System.Threading.Tasks;

namespace Hosting
{
    public delegate Task RequestDelegate(HttpContext context);
}