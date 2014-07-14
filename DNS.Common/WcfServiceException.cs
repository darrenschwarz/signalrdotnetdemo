using System.ServiceModel;

namespace DNS.Common
{
    public class WcfServiceException : FaultException
    {
        private readonly string _message;

        public WcfServiceException(string reason)
            : base(reason)
        {
            _message = reason;
        }

        public override string Message
        {
            get { return _message; }
        }
    }
}