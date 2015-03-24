using System.Resources;
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

                _workerService.UpdateWorker(workerUpdate);

                return null;
            };

        }


        private Response GetRating()
        {
            return string.Format("Rating - {0:00}", AverageRating);
        }

        
    }
}