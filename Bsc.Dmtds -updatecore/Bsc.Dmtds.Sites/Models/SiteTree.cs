using System.Collections.Generic;

namespace Bsc.Dmtds.Sites.Models
{
    public class SiteNode
    {
        public Site Site { get; set; }
        public bool IsOnLine
        {
            get
            {
                return Services.ServiceFactory.SiteManager.IsOnline(Site.FullName);
            }
        }
        public IEnumerable<SiteNode> Children { get; set; }
    }
    public class SiteTree
    {
        public SiteNode Root { get; set; }
    }
}