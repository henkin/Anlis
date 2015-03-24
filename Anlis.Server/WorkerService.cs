using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Anlis.Server
{
    public delegate void WorkerStatusChangeHandler(WorkerStatus status);

    public interface IWorkerService
    {
        void UpdateWorker(WorkerStatus workerUpdate);

        List<WorkerStatus> GetWorkers();

        event WorkerStatusChangeHandler OnWorkerConnected;
        event WorkerStatusChangeHandler OnWorkerDisconnected;
    }

    public class WorkerService : IWorkerService
    {
        public delegate void WorkerStatusChangeHandler(WorkerStatus status);

        public event Server.WorkerStatusChangeHandler OnWorkerConnected = (x) => { };
        public event Server.WorkerStatusChangeHandler OnWorkerDisconnected = (x) => { };

        private static List<WorkerStatus> _workers = new List<WorkerStatus>();
        private Dictionary<string, int> _workerTimers = new Dictionary<string, int>();

        public List<WorkerStatus> GetWorkers() { return _workers; }

        public WorkerService()
        {
            Task.Run(() =>
                     {
                         var periodInMilliseconds = 1000;

                         while (true)
                         {
                             var keys = _workerTimers.Keys.ToList();
                             if (keys.Any(t => t != null))
                             {
                                 // mark workers that haven't sent an update in the last second as disconnected
                                 foreach (var key in keys)
                                 {
                                     _workerTimers[key] += periodInMilliseconds;
                                     //if (_workerTimers[key] > periodInMilliseconds)

                                     if (_workerTimers[key] > periodInMilliseconds)
                                         DisconnectWorker(_workers.Single((w) => w.Key == key));
                                 }
                                 Thread.Sleep(periodInMilliseconds);
                             }
                         }
                     });
        }

        private void DisconnectWorker(WorkerStatus worker)
        {
                _workers.Remove(worker);
             _workerTimers.Remove(worker.Key);
             Console.WriteLine(worker + " disconnected");
             OnWorkerDisconnected(worker);
        }

        public void UpdateWorker(WorkerStatus workerUpdate)
        {
            var worker = _workers.SingleOrDefault(w => w.Key == workerUpdate.Key);
            if (worker == default(WorkerStatus))
            {
                // add to repository of workers
                _workers.Add(workerUpdate);

                // start timer 
                _workerTimers.Add(workerUpdate.Key, 0);

                OnWorkerConnected(workerUpdate);

                Console.WriteLine(workerUpdate + " connected");
            }
            else
            {
                // reset timer
                _workerTimers[workerUpdate.Key] = 0;

            }
            
        }

        private void StoreInformation(WorkerStatus workerUpdate)
        {


        }

        private void ResetWorkerTimer(WorkerStatus workerUpdate)
        {


        }
    }
}