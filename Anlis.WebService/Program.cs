using Anlis.Server;
using Topshelf;

namespace Anlis.Service.Web
{
    public static class Program
    {
        


        public static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<SiteApplication>(s =>                      
                {
                    s.ConstructUsing(name => new SiteApplication(args));    
                    s.WhenStarted(tc => tc.Start());             
                    s.WhenStopped(tc => tc.Stop());              
                });
                x.RunAsLocalSystem();                         

                x.SetDescription("Anlis Server Process");      
                x.SetDisplayName("Anlis.Server");                  
                x.SetServiceName("Anlis.Server");

                x.UseNLog();
            });    
        }
    }
}
