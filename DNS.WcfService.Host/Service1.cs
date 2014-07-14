using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using Castle.Core.Logging;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;

namespace Dns.WcfService.Host
{
    public class Service1 : BaseService<Service1>//ServiceBase
    {
        public static void Main()
        {
            BootstrapServiceContainer();
        }

        protected override void OnStart(string[] args)
        {
            //Console.WriteLine("Server running");            
        }

        protected override void OnStop()
        {

        }

        //private static IWindsorContainer PopulateContainer()
        //{
        //    var container = new WindsorContainer();

        //    container.AddFacility<WcfFacility>();

        //    container.Register(Component.For<IWcfService>()
        //        .ImplementedBy<WcfService>()
        //        .AsWcfService(
        //            new DefaultServiceModel()
        //                .AddEndpoints(
        //                    WcfEndpoint.BoundTo(
        //                        new NetTcpBinding()).At("net.tcp://localhost:8002/WcfService")
        //                )));


        //    container.Register(Component.For<IWindsorContainer>().Instance(container));

        //    return container;
        //}

        private static bool IsValid(IWindsorContainer container, ILogger logger = null)
        {
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            var diagnostic = new PotentiallyMisconfiguredComponentsDiagnostic(container.Kernel);

            var handlers = diagnostic.Inspect();
            if (handlers == null || !handlers.Any()) return true;

            var builder = new StringBuilder();

            builder.AppendFormat("Misconfigured components ({0})\r\n", handlers.Count());

            foreach (var handler in handlers)
            {
                var info = (IExposeDependencyInfo)handler;
                var inspector = new DependencyInspector(builder);
                info.ObtainDependencyDetails(inspector);
            }
            Console.Write(builder);
            logger.Error(builder.ToString());
            return false;
        }

        protected override void Init(BaseService<Service1> service, WindsorContainer c)
        {
            try
            {
                using (var container = PopulateContainer(c, null))
                {
                    //if (!IsValid(container, Logger))
                    //{
                    //    Logger.Error("Container not valid.");
                    //    if (Environment.UserInteractive)
                    //    {
                    //        Console.ReadKey(true);
                    //    }
                    //    return;
                    //}
                    //if (Environment.UserInteractive)
                    //{
                    //    ((Service1)service).OnStart(null);

                    //    Console.WriteLine("Dns.WcfService.Host is running.");
                    //    Console.WriteLine("Press <ENTER> to terminate.");
                    //    Console.ReadKey(true);

                    //    ((Service1)service).OnStop();
                    //}
                    //else
                        Run(service);
                }
            }
            catch (AddressAlreadyInUseException aaiue)
            {
                Logger.Error(aaiue.Message);
                Logger.Error(aaiue.InnerException.Message);
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                //Console.WriteLine(ex.Message);
                //Console.ReadLine();
            } //NOTE [Darren,20140710] 
        }

        protected override WindsorContainer PopulateContainer(WindsorContainer c, IParameter parameter)
        {
            var container = c;

            container.AddFacility<WcfFacility>();

            container.Register(Component.For<IWcfService>()
                .ImplementedBy<WcfService>()
                .AsWcfService(
                    new DefaultServiceModel()
                        .AddEndpoints(
                            WcfEndpoint.BoundTo(
                                new NetTcpBinding()).At("net.tcp://localhost:8002/WcfService")
                        )));


            //container.Register(Component.For<IWindsorContainer>().Instance(container));

            return container;
        }
    }
}
