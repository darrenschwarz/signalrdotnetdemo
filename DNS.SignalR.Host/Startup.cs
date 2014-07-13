using Microsoft.Owin.Cors;
using Owin;

namespace DNS.SignalR.Host
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}