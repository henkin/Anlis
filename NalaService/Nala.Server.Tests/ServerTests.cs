//using System;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using RestSharp;

//namespace Anlis.Server.Tests
//{
//    [TestFixture]
//    public class ServerTests
//    {
//        [Test]
//        public void PostWorkerInfo()
//        {
//            //// client.Authenticator = new HttpBasicAuthenticator(username, password);
//            const int serverPort = 7887;

//            Task.Run(() =>
//            {
//                using (StringWriter sw = new StringWriter())
//                using (TextReader serverReader = new StringReader(""))
//                {
//                    Console.SetOut(sw);
//                    //Console.SetIn(serverReader);
//                    //Anlis.Server.Program.Main(new[] { serverPort.ToString() });

//                    //httpClient.Post(
//                    //   "",
//                    //   new StringContent(
//                    //       myObject.ToString(),
//                    //       Encoding.UTF8,
//                    //       "application/json"));
//                    //       }
//                    //   }

//                    string expected = string.Format("Running on http://+:" + serverPort);
//                    Assert.AreEqual(expected,
//                        sw.ToString().Trim().Split(Environment.NewLine.ToCharArray()).First());

//                }
//            });

//            Thread.Sleep(300);

//            var client = new RestClient("http://localhost:" + serverPort);
//            //var request = new RestRequest("resource/{id}", Method.POST);
//            var request = new RestRequest("/register", Method.POST);
//            request.AddBody(new WorkerAddress()
//            {
//                IP = "192.168.9.9",
//                Name = "Unit Test",
//                Port = 1234,
//            });
//            request.RequestFormat = DataFormat.Json;
//            var response = client.Execute(request);
//            Assert.AreEqual(ResponseStatus.Completed, response.ResponseStatus);
//            var content = response.Content; // raw content as string
//            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
//            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

//        }
//    }
//}