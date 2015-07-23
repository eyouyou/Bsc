using System.Collections.Generic;
using Bsc.Dmtds.Sites.Caching;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.Caching
{
    public class ViewProvider : SiteElementProviderBase<Bsc.Dmtds.Sites.Models.View>, IViewProvider
    {
        #region .ctor
        private IViewProvider inner;
        public ViewProvider(IViewProvider innerRepository)
            : base(innerRepository)
        {
            this.inner = innerRepository;
        }
        #endregion

        #region GetItemCacheKey
        protected override string GetItemCacheKey(Models.View o)
        {
            return string.Format("View:{0}", o.Name.ToLower());
        }

        #endregion

        #region Localize
        public void Localize(Models.View o, Models.Site targetSite)
        {
            try
            {
                inner.Localize(o, targetSite);
            }
            finally
            {
                ClearObjectCache(targetSite);
            }
        }
        #endregion

        #region Export
        public void Export(IEnumerable<Models.View> sources, System.IO.Stream outputStream)
        {
            inner.Export(sources, outputStream);
        }
        #endregion

        #region Import
        public void Import(Models.Site site, System.IO.Stream zipStream, bool @override)
        {
            try
            {
                inner.Import(site, zipStream, @override);
            }
            finally
            {
                site.ClearCache();
            }
        }
        #endregion

        #region Copy
        public Models.View Copy(Models.Site site, string sourceName, string destName)
        {
            try
            {
                return inner.Copy(site, sourceName, destName);
            }
            finally
            {
                ClearObjectCache(site);
            }
        }
        #endregion

        #region ISiteElementProvider InitializeToDB/ExportToDisk
        public void InitializeToDB(Site site)
        {
            //not need to implement.
        }

        public void ExportToDisk(Site site)
        {
            //not need to implement.
        }
        #endregion
    }
}