using System.Collections.Generic;
using Bsc.Dmtds.Sites.Caching;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.Caching
{
    public class UrlKeyMapProvider : SiteElementProviderBase<UrlKeyMap>, IUrlKeyMapProvider
    {
        #region .ctor
        private IUrlKeyMapProvider inner = null;
        public UrlKeyMapProvider(IUrlKeyMapProvider innerRepository)
            : base(innerRepository)
        {
            inner = innerRepository;
        }
        #endregion

        #region Export
        public void Export(Site site, System.IO.Stream outputStream)
        {
            inner.Export(site, outputStream);
        }
        #endregion

        #region Import
        public void Import(Site site, System.IO.Stream zipStream, bool @override)
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

        #region GetListCacheKey
        protected override string GetListCacheKey()
        {
            return "UrlKeyMapList";
        }
        #endregion

        #region GetItemCacheKey
        protected override string GetItemCacheKey(UrlKeyMap o)
        {
            return "UrlKeyMap:" + o.Key.ToLower();
        }
        #endregion

        #region All
        public override IEnumerable<UrlKeyMap> All()
        {
            return inner.All();
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