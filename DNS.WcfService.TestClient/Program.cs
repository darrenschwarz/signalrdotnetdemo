using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Dns.WcfService.Host;

namespace DNS.WcfService.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var c = BuildContainer())
            {
                Console.WriteLine("Client is running.");  
                while (true)
                {
                    var service = c.Resolve<IWcfService>();

                    DoSomething(service);

                    Console.WriteLine("Did something....");
                    Console.ReadKey(true);

                }
            }
        }

        private async static void DoSomething(IWcfService service)
        {
            var t = Task.Factory.StartNew(service.DoSomething);
             
            await t;            
        }


        private static IWindsorContainer BuildContainer()
        {
            var container = new WindsorContainer();

            container.AddFacility<WcfFacility>();

            container.Register(Component.For<IWcfService>()
                .AsWcfClient(
                    new DefaultClientModel(
                        WcfEndpoint.BoundTo(
                            new NetTcpBinding()).At("net.tcp://localhost:8002/WcfService"))
                ));

            return container;
        }
    }
}
