namespace Hosting
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseImages(@"C:\Users\johns\Pictures\Camera Roll");
        }
    }
}