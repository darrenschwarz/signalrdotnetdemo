using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace DNS.SignalR.Host
{
    public partial class Service1 : ServiceBase
    {     
        public Service1()
        {
            InitializeComponent(); 
        }

        protected override void OnStart(string[] args)
        {
      
        }

        protected override void OnStop()
        {

        }
    }
}
