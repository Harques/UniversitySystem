using System;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using WebApplication1.Mappings;
using WebApplication1.Models.DTOS.Create;

[assembly: OwinStartup(typeof(WebApplication1.App_Start.Startup))]

namespace WebApplication1.App_Start
{
    // In this class we will Configure the OAuth Authorization Server.
    public class Startup
    {
        //public void ConfigureServices(IServiceCollection services)
        //{ 
        //    services.AddControllers();
        //    services.AddAutoMapper(typeof(Startup).Assembly);
        //}

        public void Configuration(IAppBuilder app)
        {
            // Enable CORS (cross origin resource sharing) for making request using browser from different domains
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Token
                TokenEndpointPath = new PathString("/token"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new MyAuthorizationServerProvider()
            };
            //Token Generations
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}