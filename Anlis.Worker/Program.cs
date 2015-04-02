using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Topshelf;

namespace Anlis.Worker
{
    class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            
            HostFactory.Run(x =>
            {
                x.Service<WorkerApplicaiton>(s =>
                {
                    s.ConstructUsing(name => new WorkerApplicaiton());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Anlis Worker - NLP Services");
                x.SetDisplayName("Anlis.Worker");
                x.SetServiceName("Anlis.Worker");

                x.UseNLog();
            });       
        }
    }
}
