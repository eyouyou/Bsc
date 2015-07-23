using System.Collections.Generic;

namespace Bsc.Dmtds.Core.Mvc.WebResourceLoader.DynamicClientResource
{
    public class DynamicClientResourceFactory
    {
        public static DynamicClientResourceFactory Default = (DynamicClientResourceFactory)TypeActivator.CreateInstance(typeof(DynamicClientResourceFactory));

        private IList<IDynamicClientResource> providers = new List<IDynamicClientResource>();
        public virtual void RegisterDynamicCssProvider(IDynamicClientResource dynamicCss)
        {
            providers.Add(dynamicCss);
        }
        public virtual IEnumerable<IDynamicClientResource> ResolveAllProviders()
        {
            return providers;
        }
        public virtual IDynamicClientResource ResolveProvider(string filePath)
        {
            foreach (var item in ResolveAllProviders())
            {
                if (item.Accept(filePath))
                {
                    return item;
                }
            }
            return null;
        }
 
    }
}