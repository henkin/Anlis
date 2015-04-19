using System;
using System.CodeDom;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Nala.Client
{
    public class StatementAnalysisResponse
    {
        public string statement { get; set; }
        public ParsedStatement parse { get; set; }
    }

    public class ParsedStatement
    {

        public string xml;
        public StatementTree tree { get; set; }
        public Dependency[] dependencies { get; set; }
        public override string ToString()
        {
            return xml;
        }
    }

    public class StatementTree
    {
        public StatementNode node { get; set; }
    }

    public class Leaf
    {
        public string value;
    }

    public class StatementNode
    {
        public string value;
        public StatementNode[] nodes { get; set; }
        public Leaf leaf { get; set; }
    }

    public class Dependency
    {
        public string governor { get; set; }
        public string dependent { get; set; }
    }


    public class Service
    {


        public class StatementAnalysisRequest
        {
            public StatementAnalysisRequest(string statement)
            {
                Statement = statement;
            }

            public string Statement { get; set; }
        }
        private string _baseUrl = "http://localhost:7889";
        //private string _baseUrl = "http://ipv4.fiddler:7889";
        

        public Service()
        {

        }

        public Service(string url) : this()
        {
            _baseUrl = url;
        }

        public async Task<StatementAnalysisResponse> AnalyzeStatement(string statement)
        {
            var request = new StatementAnalysisRequest(statement);
            var client = new RestClient(_baseUrl) { Timeout = 10000};

            var restRequest = new RestRequest("/statements", Method.POST);
            restRequest.RequestFormat = DataFormat.Json; 
            restRequest.AddBody(request);
                //var response = await client.ExecuteTaskAsync<StatementAnalysisResponse>(restRequest);
                //return response.Data;
         
            try
            {

                var response = await client.ExecuteTaskAsync(restRequest);

                var package = SimpleJson.DeserializeObject<StatementAnalysisResponse>(response.Content);
 
                //if (!string.IsNullOrEmpty(package.error))
                //{
                //    _logService.WarnFormat(@"Error response for API ({0}{1}) with error message ({2})", _client.BaseUrl,
                //        NestedUrl, package.error);
                //    result = package;
                //}
                //else
                //{
                //    _logService.DebugFormat(@"API ({0}{1}) call succeeded", _client.BaseUrl, NestedUrl);
                //    result = package;
                //}
                return package;
            }
            catch (Exception ex)
            {
                //.ErrorFormat(ex, @"Error occurred while trying API ({0}{1}))", _client.BaseUrl, NestedUrl);
                throw new Exception("Service call failed", ex);
            }
            
        }

    }
}

