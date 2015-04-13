using Owin;
using Nancy;

namespace Nala.Service.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
			StaticConfiguration.DisableErrorTraces = false;
        }
    }
}