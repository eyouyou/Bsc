using System.Collections.Generic;
using System.Linq;

namespace Bsc.Dmtds.Sites.Models
{
    public class LabelPath : DirectoryResource
    {
        public LabelPath(Site site)
            : base(site, "Labels")
        {
        }

        public override IEnumerable<string> RelativePaths
        {
            get { yield return ""; }
        }

        public override IEnumerable<string> ParseObject(IEnumerable<string> relativePaths)
        {
            return relativePaths.Take(relativePaths.Count() - 1);
        }
    }
}