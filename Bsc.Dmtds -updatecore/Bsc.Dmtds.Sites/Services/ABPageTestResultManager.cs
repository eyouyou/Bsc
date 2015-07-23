using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Sites.ABTest;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;

namespace Bsc.Dmtds.Sites.Services
{
    public class ABPageTestResultManager : ManagerBase<ABPageTestResult, IABPageTestResultProvider>
    {
        #region .ctorISiteElementProvider<ABPageSetting>
        static System.Collections.Concurrent.ConcurrentDictionary<string, ABPageTestResult> results = new System.Collections.Concurrent.ConcurrentDictionary<string, ABPageTestResult>(StringComparer.OrdinalIgnoreCase);

        IABPageTestResultProvider _provider;
        IABPageSettingProvider _pageVisitRuleProvider;
        public ABPageTestResultManager(IABPageTestResultProvider provider, IABPageSettingProvider pageVisitRuleProvider)
            : base(provider)
        {
            _provider = provider;
            _pageVisitRuleProvider = pageVisitRuleProvider;
        }
        #endregion

        #region All
        public override IEnumerable<ABPageTestResult> All(Site site, string filterName)
        {
            var list = _provider.All(site);
            if (!string.IsNullOrEmpty(filterName))
            {
                list = list.Where(it => it.UUID.Contains(filterName));
            }
            return list;
        }
        #endregion

        #region Get
        public override ABPageTestResult Get(Site site, string name)
        {
            return _provider.Get(new ABPageTestResult() { Site = site, UUID = name });
        }
        #endregion

        #region Update
        public override void Update(Site site, ABPageTestResult @new, ABPageTestResult old)
        {
            @new.Site = site;
            old.Site = site;
            _provider.Update(@new, old);
        }

        #endregion

        #region IncreaseShowTime
        public virtual void IncreaseShowTime(Site site, string pageVisitRule, string matchedPage)
        {
            IncreaseHelper(site, pageVisitRule, matchedPage, (result, hit) =>
            {
                if (hit == null)
                {
                    hit = new ABPageTestHits() { PageName = matchedPage, ShowTimes = 1, HitTimes = 0 };
                    result.PageHits.Add(hit);
                }
                else
                {
                    hit.ShowTimes = hit.ShowTimes + 1;
                }

                result.TotalShowTimes = result.TotalShowTimes + 1;
            });
        }
        #endregion

        private void IncreaseHelper(Site site, string pageVisitRule, string matchedPage, Action<ABPageTestResult, ABPageTestHits> increaseFunc)
        {
            ABPageTestResult abPageTestResult = null;

            abPageTestResult = results.GetOrAdd(pageVisitRule, (key) =>
            {
                abPageTestResult = Get(site, key);
                if (abPageTestResult == null)
                {
                    abPageTestResult = new ABPageTestResult() { Site = site, ABPageUUID = key, PageHits = new List<ABPageTestHits>() };

                    this.Add(site, abPageTestResult);
                }
                return abPageTestResult;
            });
            if (abPageTestResult != null)
            {
                lock (abPageTestResult)
                {
                    var pageHit = abPageTestResult.PageHits.Where(it => it.PageName.EqualsOrNullEmpty(matchedPage, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                    increaseFunc(abPageTestResult, pageHit);

                    this.Update(site, abPageTestResult, abPageTestResult);
                }
            }
        }

        #region 增加点击时间
        public virtual void IncreaseHitTime(Site site, string pageVisitRule, string matchedPage)
        {
            IncreaseHelper(site, pageVisitRule, matchedPage, (result, hit) =>
            {

                if (hit == null)
                {
                    hit = new ABPageTestHits() { PageName = matchedPage, ShowTimes = 0, HitTimes = 1 };
                    result.PageHits.Add(hit);
                }
                else
                {
                    hit.HitTimes = hit.HitTimes + 1;
                }

                result.TotalHitTimes = result.TotalHitTimes + 1;
            });
        }
        #endregion
    }
}