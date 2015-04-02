using Owin;

namespace Anlis.Service.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}