using System.Collections.Generic;

namespace Bsc.Dmtds.Sites.Models
{
    public class SiteMapNode
    {
        public Page Page { get; set; }
        public IEnumerable<SiteMapNode> Children { get; set; }
    }
    public class SiteMap
    {
        public SiteMapNode Root { get; set; }
    }

}