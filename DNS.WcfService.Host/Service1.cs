using System;
using System.ServiceProcess;

namespace Dns.WcfService.Host
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Server running");            
        }

        protected override void OnStop()
        {

        }
    }
}
