using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using NLog;
using System.Threading.Tasks;
using System.Threading;

namespace Nala.Service.Web
{
    public class SiteApplication
    {
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        IDisposable _webApp;
        string _url;

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
			_logger.Warn("Running on {0}", _url);
			_logger.Warn("Ctrl-C to exit");
        }

        public void Stop()
        {
			_logger.Warn("Exiting.");
            if (_webApp != null)
                _webApp.Dispose();
            _webApp = null;
        }
    }
}