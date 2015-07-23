using System.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Services
{
    [Dependency(typeof(RepositoryTemplateManager))]
    public class RepositoryTemplateManager : ItemTemplateManager
    {
        private IBaseDir _baseDir;
        public RepositoryTemplateManager(IBaseDir baseDir)
        {
            _baseDir = baseDir;
        }
        protected override string TemplatePath
        {
            get { return Path.Combine(_baseDir.DataPhysicalPath, "ImportedContents"); }
        }

    }
}