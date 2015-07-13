using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(R_WebSite1.Startup))]
namespace R_WebSite1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
