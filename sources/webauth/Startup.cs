using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace webauth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            //首先，在DI中注册服务认证所需的服务：
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(o =>
                {
                    o.ClientId = "server.hybrid";
                    o.ClientSecret = "secret";
                    o.Authority = "https://demo.identityserver.io/";
                    o.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //然后，，注册认证中间件
            app.UseAuthentication();

            //如上，我们的系统便支持了Cookie和JwtBearer两种认证方式，是不是非常简单，在我们的应用程序中使用认证系统时，只需要调用 上一章 介绍的 HttpContext 中认证相关的扩展方法即可。








            //// 创建一个用户身份，注意需要指定AuthenticationType，否则IsAuthenticated将为false。
            //var claimIdentity = new ClaimsIdentity("myAuthenticationType");
            //// 添加几个Claim
            //claimIdentity.AddClaim(new Claim(ClaimTypes.Name, "bob"));
            //claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "bob@gmail.com"));
            //claimIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, "18888888888"));

            ////如上，我们可以根据需要添加任意个的Claim，最后我们还需要再将用户身份放到ClaimsPrincipal对象中。




            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
