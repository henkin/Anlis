using System;
using System.Resources;
using System.Threading.Tasks;
using Anlis.Core;
using Nancy;
using Nancy.ModelBinding;

namespace Anlis.Server
{
    public class MainModule : NancyModule
    {
        private static readonly INlpService _nlpService;
        private static int? _averageRating;
        private static readonly IWorkerService _workerService;

        public int AverageRating
        {
            get
            {
                if (!_averageRating.HasValue)
                    _averageRating = _nlpService.GetAverageRating();

                return _averageRating.Value;
            }
        }

        static MainModule()
        {
            _nlpService = new NlpService();
            _workerService = new WorkerService();
        }

        public MainModule()
        {

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

            Get["/", true] = async (x, ct) => GetRating();

            Post["/worker", true] = async (_, ct) =>
            {
                var workerUpdate = this.Bind<WorkerStatus>();

                await _workerService.UpdateWorker(workerUpdate);

                return null;
            };

            Post["/statements", true] = async (_, ct) =>
            {
                var statements = this.Bind<ParsedStatementRequest>();
                var response = await GetParsedStatementResponse(statements);
                return response;
            };
}

        private async Task<ParsedStatementResponse> GetParsedStatementResponse(ParsedStatementRequest statements)
        {
            // distribute the work among the servers 
            
            // count the complexity along with how long it took. 

            // 
            Console.WriteLine(statements);

            //- and remembr the results.
            return null;
        }


        private Response GetRating()
        {
            return string.Format("Rating - {0:00}", AverageRating);
        }

        
    }

    internal class ParsedStatementResponse
    {
    }

    public class ParsedStatementRequest
    {
    }
}