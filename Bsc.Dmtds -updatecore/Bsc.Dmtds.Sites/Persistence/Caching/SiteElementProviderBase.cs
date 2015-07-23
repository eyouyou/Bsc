using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Caching;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.Caching
{
    public abstract class SiteElementProviderBase<T> : ProviderBase<T>, ISiteElementProvider<T>
       where T : class, IPersistable, IIdentifiable
    {
        #region .ctor
        private ISiteElementProvider<T> innerProvider;
        public SiteElementProviderBase(ISiteElementProvider<T> inner)
            : base(inner)
        {
            this.innerProvider = inner;
        }
        #endregion

        #region All
        public virtual IEnumerable<T> All(Models.Site site)
        {
            var cacheKey = GetListCacheKey();
            if (!string.IsNullOrEmpty(cacheKey))
            {
                return GetObjectCache(site).GetCache(cacheKey, () => innerProvider.All(site).ToArray());
            }
            else
            {
                return innerProvider.All(site);
            }

        }
        #endregion


        #region ISiteElementProvider InitializeToDB/ExportToDisk
        public void InitializeToDB(Site site)
        {
            try
            {
                innerProvider.InitializeToDB(site);
            }
            finally
            {
                ClearObjectCache(site);
            }
        }

        public void ExportToDisk(Site site)
        {
            innerProvider.ExportToDisk(site);
        }
        #endregion
    }
}