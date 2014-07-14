using System;
using System.ServiceModel;
using Castle.Windsor;
using DNS.Common;
using Microsoft.Owin.Hosting;

namespace DNS.SignalR.Host
{
    public class Service : BaseService<Service>
    {
        const string url = "http://localhost:8080";

        public static void Main()
        {
            BootstrapServiceContainer();
        }

        protected override void OnStart(string[] args)
        {
            WebApp.Start(url);
            Logger.Debug("Service starting.");
            Logger.Debug("Service started.");
        }

        protected override void OnStop()
        {
            Logger.Debug("Service stopping.");
            Logger.Debug("Service stopped.");
        }

        protected override void Init(BaseService<Service> service, WindsorContainer c)
        {
            try
            {
                using (var container = PopulateContainer(c, null))
                {
                    if (!WindsorContainerValidator.IsValid(container, Logger))
                    {
                        Logger.Error("Container not Valid");
                        if (Environment.UserInteractive)
                        {
                            Console.ReadKey(true);
                        }
                        return;
                    }
                    if (Environment.UserInteractive)
                    {
                        ((Service)service).OnStart(null);

                        Console.WriteLine("DNS.SignalR.Host is running.");
                        Console.WriteLine("Press <ENTER> to terminate.");
                        Console.ReadKey(true);

                        ((Service)service).OnStop();
                    }
                    else
                        Run(service);
                }
            }
            catch (AddressAlreadyInUseException aaiue)
            {
                Logger.Error(aaiue.Message);
                Logger.Error(aaiue.InnerException.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        protected override WindsorContainer PopulateContainer(WindsorContainer container, IParameter parameter)
        {
           
            return container;
        }
    }
}
