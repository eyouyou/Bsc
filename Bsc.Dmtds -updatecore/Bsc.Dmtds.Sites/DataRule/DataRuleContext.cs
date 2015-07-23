using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.DataRule
{
    public class DataRuleContext
    {
        public DataRuleContext(Site site, Page page)
        {
            this.Site = site;
            this.Page = page;
        }
        public Site Site { get; private set; }
        public Page Page { get; private set; }
        public IValueProvider ValueProvider { get; set; }
    }
}