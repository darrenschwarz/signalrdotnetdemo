using System;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Castle.Core.Logging;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;

namespace Dns.WcfService.Host
{
    internal class Program
    {
        //private static void Main(string[] args)
        //{
        //    using (var c = PopulateContainer())
        //    {
        //        if (!IsValid(c))
        //        {
        //            Console.WriteLine("Container not valid");
        //            Console.ReadKey(true);
        //            return;
        //        }

        //        Console.WriteLine("Dns.WcfService.Host is running");
        //        Console.WriteLine("Press <ENTER> to terminate");
        //        Console.ReadKey(true);
        //    }
        //}

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

        //private static bool IsValid(IWindsorContainer container, ILogger logger = null)
        //{
        //    if (logger == null)
        //    {
        //        logger = NullLogger.Instance;
        //    }
        //    var diagnostic = new PotentiallyMisconfiguredComponentsDiagnostic(container.Kernel);

        //    var handlers = diagnostic.Inspect();
        //    if (handlers == null || !handlers.Any()) return true;

        //    var builder = new StringBuilder();

        //    builder.AppendFormat("Misconfigured components ({0})\r\n", handlers.Count());

        //    foreach (var handler in handlers)
        //    {
        //        var info = (IExposeDependencyInfo)handler;
        //        var inspector = new DependencyInspector(builder);
        //        info.ObtainDependencyDetails(inspector);
        //    }
        //    Console.Write(builder);
        //    logger.Error(builder.ToString());
        //    return false;
        //}
    }
}
