using System.Web;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.ABTest
{
    public class SiteMatchedContext
    {
        public HttpContextBase HttpContext { get; set; }
        public Site RawSite { get; set; }
        public Site MatchedSite { get; set; }
        public ABSiteSetting SiteVisitRule { get; set; }
        public ABSiteRuleItem MatchedRuleItem { get; set; }
    }
    public interface ISiteVisitRuleMatchedObserver
    {
        void OnMatched(SiteMatchedContext matchedContext);

    }
}