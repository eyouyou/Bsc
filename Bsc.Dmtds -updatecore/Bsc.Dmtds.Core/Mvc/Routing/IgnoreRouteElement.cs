using System.Configuration;

namespace Bsc.Dmtds.Core.Mvc.Routing
{
    public class IgnoreRouteElement : ConfigurationElement
    {
        public IgnoreRouteElement() { }
        public IgnoreRouteElement(string elementName)
        {
            Name = elementName;
        }
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("constraints", IsRequired = false)]
        public RouteChildElement Constraints
        {
            get { return (RouteChildElement)this["constraints"]; }
            set { this["constraints"] = value; }
        }
    }
}