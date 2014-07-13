using System.ServiceModel;
using System.Threading.Tasks;

namespace Dns.WcfService.Host
{
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract, FaultContract(typeof(WcfServiceException))]
        void DoSomething();
    }
}