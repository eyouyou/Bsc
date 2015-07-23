using System.Collections.Generic;

namespace Bsc.Dmtds.Sites.Models
{
    public class UrlKeyMapsFile: FileResource
    {
        public static string UrlKeyMapFileName = "UrlKeyMaps.config";
        public UrlKeyMapsFile(string physicalPath)
            : base(physicalPath)
        {

        }
        public UrlKeyMapsFile(Site site)
            : base(site, UrlKeyMapFileName)
        {

        }

        public override IEnumerable<string> RelativePaths
        {
            get { yield return ""; }
        }
    }
}