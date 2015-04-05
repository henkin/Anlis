using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Nala.Server
{
    public static class Program
    {
        public class ServerService
        {
            private IDisposable _webApp;
            private string _url;

            public ServerService(string[] args)
            {
                int port = 7887;

                if (ConfigurationManager.AppSettings["port"] != null)
                    int.TryParse(ConfigurationManager.AppSettings["port"], out port);

                _url = "http://+:" + port;
            }

            public void Start()
            {
                _webApp = WebApp.Start<Startup>(_url);
                Console.WriteLine("Running on {0}", _url);
                Console.WriteLine("Ctrl-C to exit");
                Console.ReadLine();
            }

            public void Stop()
            {
                Console.WriteLine("Exiting.");
                if (_webApp != null)
                    _webApp.Dispose();
                _webApp = null;
            }
        }


        public static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<ServerService>(s =>                      
                {
                    s.ConstructUsing(name => new ServerService(args));    
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
