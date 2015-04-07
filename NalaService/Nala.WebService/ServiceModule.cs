using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace Nala.Service.Web
{
    public class StatementAnalysisResponse
    {
        public string Statement { get; set; }
    }

    public class StatementAnalysisRequest
    {
        public string Statement { get; set; }
    }

    public class ServiceModule : NancyModule
    {
        //private static readonly IWorkerService _workerService;

        static ServiceModule()
        {
           // _workerService = new WorkerService();
        }

		public async Task<string> GetData()
		{
			var foo = "Howdy";
			return foo;
		}
        public ServiceModule()
        {
            Get["/", true] = async (x, ct) => GetVersion(); //

            Post["/statements", true] = async (_, ct) =>
            {
                var statements = this.Bind<StatementAnalysisRequest>();
                var response = await CreateStatements(statements);
                return Response.AsJson(response);
            };

            //Before += async (ctx, ct) =>
            //    {
            //        //this.AddToLog("Before Hook Delay\n");
            //        await Task.Delay(0);

            //        return null;
            //    };

            //After += async (ctx, ct) =>
            //    {
            //        //this.AddToLog("After Hook Delay\n");
            //        await Task.Delay(0);
            //        //this.AddToLog("After Hook Complete\n");


            //    };
        }

        private string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            return version;
        }

        private async Task<StatementAnalysisResponse> CreateStatements(StatementAnalysisRequest statements)
        {
            // farm out to all deps. 

            // compose back and send out
            return new StatementAnalysisResponse() { Statement = statements.Statement };

        }
    }

}