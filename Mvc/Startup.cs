using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ARQ.Maqueta.Presentation.Mvc.Startup))]
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace ARQ.Maqueta.Presentation.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure OWIN pipeline here
           ConfigureAuth(app);
        }
    }
}