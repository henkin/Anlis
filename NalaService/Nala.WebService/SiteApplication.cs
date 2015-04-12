using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using NLog;

namespace Nala.Service.Web
{
    public class SiteApplication
    {
		private static Logger _logger = LogManager.GetCurrentClassLogger();

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
			_logger.Debug("Running on {0}", _url);
			_logger.Debug("Ctrl-C to exit");
        }

        public void Stop()
        {
			_logger.Debug("Exiting.");
            if (_webApp != null)
                _webApp.Dispose();
            _webApp = null;
        }
    }
}