using System.Configuration;
using System.IO;
using System.Xml;

namespace Bsc.Dmtds.Core.Mvc.Routing
{
    public class RouteTableSection : ConfigurationSection
    {
        public RouteTableSection()
        {
        }

        [ConfigurationProperty("ignores", IsDefaultCollection = false)]
        public IgnoreRouteCollection Ignores
        {
            get
            {
                IgnoreRouteCollection ignoresCollection =
                (IgnoreRouteCollection)base["ignores"];
                return ignoresCollection;
            }
        }

        [ConfigurationProperty("routes", IsDefaultCollection = false)]
        public RouteElementCollection Routes
        {
            get
            {
                RouteElementCollection urlsCollection =
                (RouteElementCollection)base["routes"];
                return urlsCollection;
            }
        }


        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
        }


        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            return base.SerializeSection(parentElement, name, saveMode);
        }


        #region IStandaloneConfigurationSection Members

        public void DeserializeSection(string config)
        {
            this.DeserializeSection(new XmlTextReader(new StringReader(config)));
        }

        #endregion
    }
}