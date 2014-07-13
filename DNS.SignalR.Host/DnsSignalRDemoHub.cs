using Microsoft.AspNet.SignalR;

namespace DNS.SignalR.Host
{
    public class DnsSignalRDemoHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.doSomething(message);
        }
    }
}