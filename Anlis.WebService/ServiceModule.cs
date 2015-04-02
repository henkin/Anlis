using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Anlis.Core;
using Anlis.Server;
using Nancy;
using Nancy.ModelBinding;

namespace Anlis.Service.Web
{
    public class StatementAnalysisResponse
    {
        public string Statement { get; set; }
    }

    public class StatementAnalysisRequest
    {
        public string Statement { get; set; }
    }

    public class SiteModule : NancyModule
    {
        //private static readonly IWorkerService _workerService;

        static SiteModule()
        {
           // _workerService = new WorkerService();
        }

		public async Task<string> GetData()
		{
			var foo = "Howdy";
			return foo;
		}
        public SiteModule()
        {
            Get["/", true] = async (x, ct) => "meh"; //


//            Post["/worker", true] = async (_, ct) =>
//            {
//   //             var workerUpdate = this.Bind<WorkerStatus>();
//
//                await _workerService.UpdateWorker(workerUpdate);
//
//                return null;
//            };
//
            Post["/statements", true] = async (_, ct) =>
            {
                var content = _.Statement;
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

        private async Task<StatementAnalysisResponse> CreateStatements(StatementAnalysisRequest statements)
        {
            // farm out to all deps. 

            // compose back and send out
            return new StatementAnalysisResponse() { Statement = statements.Statement };

        }
    }

}