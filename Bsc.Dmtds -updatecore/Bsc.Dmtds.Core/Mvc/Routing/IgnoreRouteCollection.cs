using System.Configuration;

namespace Bsc.Dmtds.Core.Mvc.Routing
{
    public class IgnoreRouteCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IgnoreRouteElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IgnoreRouteElement)element).Name;
        }
    }
}