using System;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Extension;
using Bsc.Dmtds.Sites.Services;

namespace Bsc.Dmtds.Sites.ABTest
{
    [Dependency(typeof(IPageRequestModule), Key = "ABTestPageRequestModule")]
    public class ABPageTestRequestModule : PageRequestModuleBase
    {
        ABPageSettingManager _pageVisitRuleManager;
        ABPageTestResultManager _abPageTestResultManager;
        public ABPageTestRequestModule(ABPageSettingManager pageVisitRuleManager, ABPageTestResultManager abPageTestResultManager)
        {
            this._pageVisitRuleManager = pageVisitRuleManager;
            this._abPageTestResultManager = abPageTestResultManager;
        }
        public override void OnResolvedPage(System.Web.Mvc.ControllerContext controllerContext, View.PageRequestContext pageRequestContext)
        {

            string pageVisitRuleName;
            string matchedPage;
            ABPageTestTrackingHelper.TryGetABTestPage(controllerContext.HttpContext.Request, pageRequestContext.Site, out pageVisitRuleName, out matchedPage);

            if (!string.IsNullOrEmpty(pageVisitRuleName) && !string.IsNullOrEmpty(matchedPage))
            {
                var pageVisitRule = _pageVisitRuleManager.Get(pageRequestContext.Site, pageVisitRuleName);
                if (pageVisitRule != null && !string.IsNullOrEmpty(pageVisitRule.ABTestGoalPage))
                {
                    if (pageVisitRule.ABTestGoalPage.EqualsOrNullEmpty(pageRequestContext.Page.FullName, StringComparison.OrdinalIgnoreCase))
                    {
                        _abPageTestResultManager.IncreaseHitTime(pageRequestContext.Site, pageVisitRuleName, matchedPage);
                    }
                }
            }
        } 
    }
}