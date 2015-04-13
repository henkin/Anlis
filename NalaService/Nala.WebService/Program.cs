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

				x.EnableServiceRecovery(r =>
				{
					//you can have up to three of these
					//r.RestartComputer(5, "message");
					r.RestartService(0);
					//the last one will act for all subsequent failures
					//r.RunProgram(7, "ping google.com");

					//should this be true for crashed or non-zero exits
					r.OnCrashOnly();

					//number of days until the error count resets
					r.SetResetPeriod(1);
				});
					
				x.UseLinuxIfAvailable();
                x.SetDescription("Nala Public WebService");      
                x.SetDisplayName("NalaWS");                  
                x.SetServiceName("NalaWS");

                x.UseNLog();
            });   

			//var app = new SiteApplication (args) {
			//};

			//app.Start ();
        }
    }
}
