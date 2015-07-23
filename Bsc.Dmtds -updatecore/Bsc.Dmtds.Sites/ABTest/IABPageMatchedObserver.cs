using System.Web;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.ABTest
{
    public class PageMatchedContext
    {
        public HttpContextBase HttpContext { get; set; }
        public Site Site { get; set; }
        public Page RawPage { get; set; }
        public Page MatchedPage { get; set; }
        public ABPageSetting ABPageSetting { get; set; }
        public ABPageRuleItem MatchedRuleItem { get; set; }
    }
    public interface IABPageMatchedObserver
    {
        void OnMatched(PageMatchedContext matchedContext);
    }
}