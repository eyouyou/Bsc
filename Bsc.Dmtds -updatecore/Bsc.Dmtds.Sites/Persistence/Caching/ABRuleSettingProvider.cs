using System.Collections.Generic;
using Bsc.Dmtds.Sites.ABTest;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.Caching
{
    public class ABRuleSettingProvider : SiteElementProviderBase<ABRuleSetting>, IABRuleSettingProvider
    {
        #region .ctor
        private IABRuleSettingProvider _provider;
        public ABRuleSettingProvider(IABRuleSettingProvider provider)
            : base(provider)
        {
            this._provider = provider;
        }
        #endregion

        #region GetListCacheKey
        protected override string GetListCacheKey()
        {
            return "All-VisitRuleSettings:";
        }
        #endregion

        #region GetItemCacheKey
        protected override string GetItemCacheKey(ABRuleSetting o)
        {
            return string.Format("VisitRuleSetting:{0}", o.Name);
        }
        #endregion

        #region Export
        public void Export(IEnumerable<ABRuleSetting> sources, System.IO.Stream outputStream)
        {
            _provider.Export(sources, outputStream);
        }
        #endregion

        #region Import
        public void Import(Models.Site site, System.IO.Stream zipStream, bool @override)
        {
            try
            {
                _provider.Import(site, zipStream, @override);
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
                _provider.InitializeToDB(site);
            }
            finally
            {
                ClearObjectCache(site);
            }
        }

        public void ExportToDisk(Site site)
        {
            _provider.ExportToDisk(site);
        }
        #endregion
    }
}