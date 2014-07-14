using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;

namespace DNS.Common
{
    public static class WindsorContainerValidator
    {
        public static bool IsValid(IWindsorContainer container, ILogger logger = null)
        {
            if (logger == null)
                logger = (ILogger)NullLogger.Instance;
            IHandler[] handlerArray = new PotentiallyMisconfiguredComponentsDiagnostic(container.Kernel).Inspect();
            if (handlerArray == null || !Enumerable.Any<IHandler>((IEnumerable<IHandler>)handlerArray))
                return true;
            StringBuilder message = new StringBuilder();
            message.AppendFormat("Misconfigured components ({0})\r\n", (object)Enumerable.Count<IHandler>((IEnumerable<IHandler>)handlerArray));
            foreach (IExposeDependencyInfo exposeDependencyInfo in handlerArray)
                exposeDependencyInfo.ObtainDependencyDetails((IDependencyInspector)new DependencyInspector(message));
            Console.Write((object)message);
            logger.Error(((object)message).ToString());
            return false;
        }
    }
}