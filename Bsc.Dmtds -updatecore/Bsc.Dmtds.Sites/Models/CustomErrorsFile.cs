using System.Collections.Generic;

namespace Bsc.Dmtds.Sites.Models
{
    public class CustomErrorsFile : FileResource
    {
        public static string CustomErrorFileName = "CustomErrors.config";
        public CustomErrorsFile(string physicalPath)
            : base(physicalPath)
        {

        }
        public CustomErrorsFile(Site site)
            : base(site, CustomErrorFileName)
        {

        }

        public override IEnumerable<string> RelativePaths
        {
            get { yield return ""; }
        }

    }
}