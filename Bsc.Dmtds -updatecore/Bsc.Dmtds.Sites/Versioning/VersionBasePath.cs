using System.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Versioning
{
    public class VersionBasePath
    {
        private static string VersionPathName = "Versions";
        public VersionBasePath(DirectoryResource dir)
        {
            var baseDir = EngineContext.Current.Resolve<IBaseDir>();
            var basePath = baseDir.DataPhysicalPath;
            var versionPath = Path.Combine(basePath, VersionPathName);
            this.PhysicalPath = dir.PhysicalPath.Replace(basePath, versionPath);
        }
        public string PhysicalPath { get; set; }
        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        } 
    }
    public class VersionPath
    {
        public VersionPath(DirectoryResource dir, int version)
        {
            var versionBasePath = new VersionBasePath(dir);
            this.PhysicalPath = Path.Combine(versionBasePath.PhysicalPath, version.ToString());
        }
        public string PhysicalPath { get; set; }
        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }
    }
}