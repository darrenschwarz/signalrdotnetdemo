using System.ServiceModel;
using DNS.Common;

namespace Dns.WcfService.Host
{
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract, FaultContract(typeof(WcfServiceException))]
        void DoSomething();
    }
}