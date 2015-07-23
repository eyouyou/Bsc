using System;
using System.IO;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Models.Paths;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Content.Versioning
{
    public class ContentVersionPath : IPath
    {
        public ContentVersionPath(TextContent content)
        {
            var contentPath = new TextContentPath(content);
            var basePath = EngineContext.Current.Resolve<IBaseDir>();
            var versionPath = Path.Combine(basePath.DataPhysicalPath, VersionPathName);
            this.PhysicalPath = contentPath.PhysicalPath.Replace(basePath.DataPhysicalPath, versionPath);
        }
        private static string VersionPathName = "Versions";
        #region IPath Members

        public string PhysicalPath
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get
            { throw new NotImplementedException(); }
        }

        public string SettingFile
        {
            get;
            private set;
        }

        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }

        public void Rename(string newName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}