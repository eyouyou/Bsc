using System;
using System.Web;
using Ninject;
using Ninject.Activation.Caching;

namespace Bsc.Dmtds.Ninject.InRequestScope
{
    public class OnePerRequestHttpModule : IHttpModule
    {
        public OnePerRequestHttpModule()
        {
            this.ReleaseScopeAtRequestEnd = true;
        }
        public void DeactivateInstancesForCurrentHttpRequest()
        {
            if (this.ReleaseScopeAtRequestEnd)
            {
                HttpContext context = HttpContext.Current;
                var kernel = GetKernel();
                if (kernel != null)
                {
                    kernel.Components.Get<ICache>().Clear(context);
                }
            }
        }
        private IKernel GetKernel()
        {
            if (Bsc.Dmtds.Core.Runtime.EngineContext.Current is NinjectEngine)
            {
                return (IKernel)((ContainerManager)((NinjectEngine)Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager).Container;
            }
            return null;
        }
        // Properties
        public bool ReleaseScopeAtRequestEnd { get; set; }
        public void Init(HttpApplication application)
        {
            application.EndRequest += delegate(object o, EventArgs e)
            {
                this.DeactivateInstancesForCurrentHttpRequest();
            };
        }
        public void Dispose()
        {
        }
    }
}