namespace Hosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseServer(new HttpListenerServerFactory())
                .UseStartup(typeof (Startup))
                .Build();

            host.Start();
        }
    }
}
