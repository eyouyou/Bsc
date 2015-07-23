using System.Configuration;

namespace Bsc.Dmtds.Core.Caching.NotifyRemote
{
    public class CacheNotificationSection : ConfigurationSection
    {
        #region Consts
        const string SECTION_NAME = "cache.notify";
        #endregion

        #region Properties
        [ConfigurationProperty("servers", IsDefaultCollection = false)]
        public ServerItemElementCollection Servers
        {
            get
            {
                ServerItemElementCollection itemsCollection =
                (ServerItemElementCollection)base["servers"];
                return itemsCollection;
            }
        }
        #endregion

        #region Methods
        public static CacheNotificationSection GetSection()
        {
            return (CacheNotificationSection)ConfigurationManager.GetSection(SECTION_NAME);
        }
        #endregion
    }
}