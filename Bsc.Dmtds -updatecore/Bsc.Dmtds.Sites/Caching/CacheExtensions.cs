using System.Runtime.Caching;
using Bsc.Dmtds.Caching;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Caching
{
    public static class CacheExtensions
    {
        public static ObjectCache ObjectCache(this Site site)
        {
            return CacheManagerFactory.DefaultCacheManager.GetObjectCache(site.GetKey());
        }
        private static string GetKey(this Site site)
        {
            return "Site:" + site.FullName.ToLower();
        }
        public static void ClearCache(this Site site)
        {
            CacheManagerFactory.ClearWithNotify(site.GetKey());
            CacheManagerFactory.DefaultCacheManager.Clear(site.GetKey());

        }
    }
}