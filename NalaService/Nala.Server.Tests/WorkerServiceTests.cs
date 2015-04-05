using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace Nala.Server.Tests
{
    [TestFixture]
    public class WorkerServiceTests
    {
        private WorkerService _workerService;
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        public WorkerServiceTests()
        {
            _workerService = new WorkerService();
        }

        [Test]
        public void WorkerConnect()
        {
            var workers = new List<WorkerStatus>() {CreateWorker(), CreateWorker(), CreateWorker()};
            for (int i = 0; i < 10; i++)
            {
                workers.ForEach(w => _workerService.UpdateWorker(w));
                Thread.Sleep(500);
            }

            workers.RemoveAt(0);
            for (int i = 0; i < 10; i++)
            {
                workers.ForEach(w => _workerService.UpdateWorker(w));
                Thread.Sleep(500);
            }
        }

        private WorkerStatus CreateWorker()
        {
            return new WorkerStatus()
            {
                IP = "0.0.0." + _random.Next(255),
                Name = "FirstOne" + _random.Next(5000),
                Port = 0
            };
        }

        [Test]
        public void WorkerDisconnect()
        {
            
        }

        [Test]
        public void WorkersList()
        {
            
        }

    }
}