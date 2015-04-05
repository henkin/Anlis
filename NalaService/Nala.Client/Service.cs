using System.Threading.Tasks;
using RestSharp;

namespace Nala.Client
{


    public class Service
    {
        public class StatementAnalysisResponse
        {
            public string Statement { get; set; }
        }

        public class StatementAnalysisRequest
        {
            public StatementAnalysisRequest(string statement)
            {
                Statement = statement;
            }

            public string Statement { get; set; }
        }
        //private string _baseUrl = "http://localhost:7889";
        private string _baseUrl = "http://ipv4.fiddler:7889";
        

        public Service()
        {

        }

        public async Task<StatementAnalysisResponse> AnalyzeStatement(string statement)
        {
            var request = new StatementAnalysisRequest(statement);
            var client = new RestClient(_baseUrl);

            var restRequest = new RestRequest("/statements", Method.POST);
            restRequest.RequestFormat = DataFormat.Json; 
            restRequest.AddBody(request);
            
            var response = await client.ExecuteTaskAsync<StatementAnalysisResponse>(restRequest);
            return response.Data;
        }
    }
}

