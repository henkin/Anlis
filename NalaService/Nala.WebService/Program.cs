using Topshelf;

namespace Nala.Service.Web
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
				//x.EnableServiceRecovery(r => { r.RestartService(1); });

                x.SetDescription("Nala WebService Main Ser");      
                x.SetDisplayName("Nala-WS");                  
                x.SetServiceName("Nala-WS");

                x.UseNLog();
            });   

			//var app = new SiteApplication (args) {
			//};

			//app.Start ();
        }
    }
}
