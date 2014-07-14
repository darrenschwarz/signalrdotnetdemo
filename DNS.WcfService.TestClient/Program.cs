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
<<<<<<< HEAD
                Console.WriteLine("Client is running.");   
=======
                Console.WriteLine("Client is running.");  
>>>>>>> a80075ae61677bb50cab34f16b708ae8ae63b3ad
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
