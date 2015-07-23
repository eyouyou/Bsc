using System.IO;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Content.Models.Paths
{
    public class RepositoryPath : IPath
    {
        public static string PATH_NAME = "Contents";
        public static string BasePhysicalPath { get; private set; }
        public static string BaseVirtualPath { get; private set; }
        static RepositoryPath()
        {
            var baseDir = EngineContext.Current.Resolve<IBaseDir>();
            BasePhysicalPath = Path.Combine(baseDir.DataPhysicalPath, PATH_NAME);
            BaseVirtualPath = UrlUtility.Combine(baseDir.DataVirtualPath, PATH_NAME);
        }
        public RepositoryPath(Repository repository)
        {
            this.PhysicalPath = Path.Combine(BasePhysicalPath, repository.Name);
            this.VirtualPath = UrlUtility.Combine(BaseVirtualPath, repository.Name);
            this.SettingFile = Path.Combine(PhysicalPath, PathHelper.SettingFileName);

        }

        #region IPath Members

        public string PhysicalPath
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get;
            private set;
        }
        public string SettingFile
        {
            get;
            private set;
        }

        #endregion

        #region IPath Members


        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }

        #endregion

        #region IPath Members


        public void Rename(string newName)
        {
            IOUtility.RenameDirectory(this.PhysicalPath, @newName);
        }

        #endregion
    }
}