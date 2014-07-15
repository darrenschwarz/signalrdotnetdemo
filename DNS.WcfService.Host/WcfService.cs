using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace Dns.WcfService.Host
{
    public class WcfService : IWcfService
    {
        public async void DoSomething()
        {
            var hubConnection = new HubConnection("http://localhost:8080/");
            var serverHub = hubConnection.CreateHubProxy("DnsSignalRDemoHub");
            hubConnection.Start().Wait();

            var t = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 20; i++)
                {
                    var ii = i;

                    serverHub.Invoke("SendMessage", string.Format("Did something...({0})", ii));    
                }
                                
            });
            
            await t;
        }
    }
}