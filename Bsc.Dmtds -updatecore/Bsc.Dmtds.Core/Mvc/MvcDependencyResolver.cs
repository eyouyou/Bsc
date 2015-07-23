using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Core.Mvc
{
    public class MvcDependencyResolver : IDependencyResolver
    {
        IEngine _engine;
        IDependencyResolver _innerResolver;

        public MvcDependencyResolver(IEngine engine, IDependencyResolver innerResolver)
        {
            _engine = engine;
            _innerResolver = innerResolver;
        }

        public object GetService(Type serviceType)
        {
            var service = _engine.TryResolve(serviceType);
            if (service == null)
            {
                service = _innerResolver.GetService(serviceType);
            }
            return service;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var services = _engine.ResolveAll(serviceType);
            if (services == null)
            {
                services = _innerResolver.GetServices(serviceType);
            }
            return services;
        }
    }
}