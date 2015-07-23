
using System.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(ImportedSiteManager))]
    public class ImportedSiteManager : ItemTemplateManager
    {
        protected override string BasePath
        {
            get
            {
                var baseDir = EngineContext.Current.Resolve<IBaseDir>();
                return Path.Combine(baseDir.DataPhysicalPath, "ImportedSites");
            }
        }
    }
}