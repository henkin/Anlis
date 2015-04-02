using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Anlis.Server;
using NLog;
using RestSharp;

namespace Anlis.Worker
{
    internal class WorkerApplicaiton
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private RestClient _restClient;
        private UpdaterService _updater;

        private static int _port = 7887;

        private static WorkerStatus GetWorkerStatus()
        {
            return new WorkerStatus()
                   {
                       IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(ip => ip.AddressFamily.ToString() == ProtocolFamily.InterNetwork.ToString()).ToString(),
                       Name = Dns.GetHostName(),
                       Port = _port,
                   };
        }

        public void Start()
        {
            var host = "http://localhost:7888";
            if (ConfigurationManager.AppSettings["ServerAddress"] != null)
                host = ConfigurationManager.AppSettings["ServerAddress"];

            _restClient = new RestClient(host);

            _logger.Warn("Starting " + Assembly.GetEntryAssembly().GetName().Version);
            //var request = new RestRequest("resource/{id}", Method.POST);
            _logger.Debug("Begin notifying " + host);

            _updater.BeginPollingForUpdate();

            while (true)
            {
                var request = new RestRequest("/worker", Method.POST);
                request.AddBody(GetWorkerStatus());
                request.RequestFormat = DataFormat.Json;
                var response = _restClient.Execute(request);
                var content = response.Content; // raw content as string
                Thread.Sleep(500);

                
            }
        }

        
        public void Stop()
        {
            _logger.Warn("Exiting.");
        }
    }

    public class UpdaterService
    {
        // http://henkin.us:51501/guestAuth/repository/download/Anlis_Anlis/lastSuccessful/Anlis.Server.zip

        public void BeginPollingForUpdate()
        {
            while (true)
            {
                //Thread.Sleep(500);

                //_updater.AttemptUpdate();
            }
        }
    }
}