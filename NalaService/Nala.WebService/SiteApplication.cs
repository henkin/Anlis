using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Nala.Service.Web
{
    public class SiteApplication
    {
        private IDisposable _webApp;
        private string _url;

        public SiteApplication(string[] args)
        {
            int port = 7889;

            if (ConfigurationManager.AppSettings["port"] != null)
                int.TryParse(ConfigurationManager.AppSettings["port"], out port);

            _url = "http://+:" + port;
        }

        public void Start()
        {

            _webApp = WebApp.Start<Startup>(_url);
            Console.WriteLine("Running on {0}", _url);
            Console.WriteLine("Ctrl-C to exit");
			while (true) {			}
        }

        public void Stop()
        {
            Console.WriteLine("Exiting.");
            if (_webApp != null)
                _webApp.Dispose();
            _webApp = null;
        }
    }
}