using System.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(SiteTemplateManager))]
    public class SiteTemplateManager : ItemTemplateManager
    {
        string basePath;
        public SiteTemplateManager(IBaseDir baseDir)
        {
            basePath = Path.Combine(baseDir.DataPhysicalPath, "SiteTemplates");
        }
        protected override string BasePath
        {
            get { return basePath; }
        }
    }
}