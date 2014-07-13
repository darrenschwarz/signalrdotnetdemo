using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace DNS.SignalR.Host
{
    class Program
    {
        const string url = "http://localhost:8080";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //static void Main()
        //{
        //    ServiceBase[] ServicesToRun;
        //    ServicesToRun = new ServiceBase[] 
        //    { 
        //        new Service1() 
        //    };
        //    ServiceBase.Run(ServicesToRun);

        //    WebApp.Start(url);
        //    Console.WriteLine("Server running on {0}", url);      

        //}

        private static void Main(string[] args)
        {
                WebApp.Start(url);
                Console.WriteLine("Server running on {0}", url);      

                Console.WriteLine("Dns.SignalR.Host is running");
                Console.WriteLine("Press <ENTER> to terminate");
                Console.ReadKey(true);
        }

    }
}
