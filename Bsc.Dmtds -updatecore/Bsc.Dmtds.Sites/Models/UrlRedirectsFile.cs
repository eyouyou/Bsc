using System.Collections.Generic;

namespace Bsc.Dmtds.Sites.Models
{
    public class UrlRedirectsFile : FileResource
    {
        public static string UrlRedirectFileName = "UrlRedirects.config";
        public UrlRedirectsFile(string physicalPath)
            : base(physicalPath)
        {

        }
        public UrlRedirectsFile(Site site)
            : base(site, UrlRedirectFileName)
        {

        }

        public override IEnumerable<string> RelativePaths
        {
            get { yield return ""; }
        }
    }
}