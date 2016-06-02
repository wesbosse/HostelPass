using hostL.API.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(hostL.API.Startup))]
namespace hostL.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            config.Formatters.JsonFormatter
            .SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            // Setup Authentication
            var authenticationOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(authenticationOptions);

            // Setup Authorization
            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true, /* In production */
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new HostLAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(authorizationOptions);
        }

    }
}