using System.Runtime.Caching;
using Bsc.Dmtds.Caching;
using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Caching
{
    public static class CacheExtensions
    {
        public static ObjectCache ObjectCache(this Repository repository)
        {
            return CacheManagerFactory.DefaultCacheManager.GetObjectCache(repository.GetKey());
        }
        private static string GetKey(this Repository repository)
        {
            return "Repository:CacheObject:" + repository.Name;
        }
        public static void ClearCache(this Repository repository)
        {
            CacheManagerFactory.ClearWithNotify(repository.GetKey());
        }
    }
}