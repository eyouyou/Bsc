using System.Collections.Generic;
using System.Configuration;

namespace Bsc.Dmtds.Core.Mvc.Routing
{
    public class RouteChildElement : ConfigurationElement
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();


        public Dictionary<string, string> Attributes
        {
            get { return this.attributes; }
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            attributes.Add(name, value);
            return true;
        }
    }
}