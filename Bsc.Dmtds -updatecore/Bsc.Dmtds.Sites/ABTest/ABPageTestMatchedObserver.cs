using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Sites.ABTest
{
    [Dependency(typeof(IABPageMatchedObserver), Key = "GoingPageObserver")]
    public class ABPageTestMatchedObserver : IABPageMatchedObserver
    {
        public void OnMatched(PageMatchedContext matchedContext)
        {
            ABPageTestTrackingHelper.SetABTestPageCookie(matchedContext);
        }
    }
}