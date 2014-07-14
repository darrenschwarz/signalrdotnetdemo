using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace DNS.Common
{
    public abstract class BaseService<T> : ServiceBase where T : BaseService<T>
    {
        private ILogger _logger = (ILogger)NullLogger.Instance;

        public ILogger Logger
        {
            get
            {
                return this._logger;
            }
            set
            {
                this._logger = value;
            }
        }

        protected static void BootstrapServiceContainer(IList<IRegistration> serviceDependencies = null)
        {
            WindsorContainer container = new WindsorContainer();
            //container.AddFacility<LoggingFacility>((Action<LoggingFacility>)(f => f.UseLog4Net("service.log4net.config")));
            container.Register(new IRegistration[1]
            {
                (IRegistration) Component.For<ServiceBase>().ImplementedBy<T>()
            });
            if (serviceDependencies != null)
                Enumerable.ToList<IRegistration>((IEnumerable<IRegistration>)serviceDependencies).ForEach((Action<IRegistration>)(x => container.Register(new IRegistration[1]
                {
                    x
                })));
            T obj = (T)container.Resolve<ServiceBase>();
            obj.Init((BaseService<T>)obj, container);
        }

        protected abstract void Init(BaseService<T> service, WindsorContainer c);

        protected abstract WindsorContainer PopulateContainer(WindsorContainer container, IParameter parameter);
    }
}