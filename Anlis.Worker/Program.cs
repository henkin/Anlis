using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Anlis.Server;
using RestSharp;

namespace Anlis.Worker
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new WorkerApplicaiton();
            Thread.Sleep(15000);
            worker.RegisterWithServer("http://localhost:7887");
        }
    }

    internal class WorkerApplicaiton
    {
        public void RegisterWithServer(string httpLocalhost)
        {

            var client = new RestClient(httpLocalhost);
            //var request = new RestRequest("resource/{id}", Method.POST);
            while (true)
            {
                var request = new RestRequest("/worker", Method.POST);
                request.AddBody(GetWorkerStatus());
                request.RequestFormat = DataFormat.Json;
                var response = client.Execute(request);
                var content = response.Content; // raw content as string
                Thread.Sleep(500);
            }
            

        }

        private static WorkerStatus GetWorkerStatus()
        {
            return new WorkerStatus()
            {
                IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(ip => ip.AddressFamily.ToString() == ProtocolFamily.InterNetwork.ToString()).ToString(),
                Name = Dns.GetHostName(),
                Port = 7888,
            };
        }
    }
}
