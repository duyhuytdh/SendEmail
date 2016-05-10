using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SendMail.Startup))]
namespace SendMail
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
