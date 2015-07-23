using System.Collections.Generic;
using Bsc.Dmtds.Sites.Caching;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.Caching
{
    public class LayoutProvider : SiteElementProviderBase<Layout>, ILayoutProvider
    {
        #region .ctor
        private ILayoutProvider inner;
        public LayoutProvider(ILayoutProvider innerRepository)
            : base(innerRepository)
        {
            inner = innerRepository;
        }
        #endregion

        #region Export
        public void Export(IEnumerable<Models.Layout> sources, System.IO.Stream outputStream)
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

        #region Localize
        public void Localize(Models.Layout o, Models.Site targetSite)
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

        #region GetItemCacheKey
        protected override string GetItemCacheKey(Layout o)
        {
            return string.Format("Layout:{0}", o.Name.ToLower());
        }
        #endregion

        #region Copy
        public Layout Copy(Site site, string sourceName, string destName)
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
            try
            {
                inner.InitializeToDB(site);
            }
            finally
            {
                ClearObjectCache(site);
            }
        }

        public void ExportToDisk(Site site)
        {
            inner.ExportToDisk(site);
        }
        #endregion
    }
}