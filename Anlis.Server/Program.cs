using System;
using System.Linq;
using Microsoft.Owin.Hosting;

namespace Anlis.Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int port = 7887;

            if (args.Any())
                int.TryParse(args.First(), out port);

            var url = "http://+:" + port;

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Ctrl-C to exit");
                Console.ReadLine();
                Console.WriteLine("Exiting.");
            }
        }
    }
}
