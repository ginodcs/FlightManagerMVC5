using System.Web.Http;
using ApiServices.App_Start;
using ApiServices.Unity;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(ApiServices.Startup))]

namespace ApiServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new UnityContainer();

            UnityConfig.Configure(container);

            var config = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(container)
            };

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            WebApiConfig.Register(config);

            ConfigureAuth(app);

            app.UseWebApi(config);
        }
    }
}
