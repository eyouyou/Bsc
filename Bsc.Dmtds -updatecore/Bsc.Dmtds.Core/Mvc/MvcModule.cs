using System;
using System.Linq;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Core.Mvc
{
    public class MvcModule : HttpApplicationEvents
    {
        public override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
            RemoveDefaultAttributeFilterProvider();

            DependencyResolver.SetResolver(new MvcDependencyResolver(EngineContext.Current, DependencyResolver.Current));
            FilterProviders.Providers.Add(new MvcDependencyAttributeFilterProvider(EngineContext.Current));
        }
        private static void RemoveDefaultAttributeFilterProvider()
        {
            var oldFilter = FilterProviders.Providers.SingleOrDefault(f => f is FilterAttributeFilterProvider);
            if (oldFilter != null)
            {
                FilterProviders.Providers.Remove(oldFilter);
            }
        }
    }
}