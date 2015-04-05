using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Anlis.Web.Startup))]
namespace Anlis.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
