using System;
using System.IO;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Content.Models.Paths
{
    public class DataPath : IPath
    {
        static string DirName = "Data";
        public DataPath(string repositoryName)
        {
            var repository = new Repository(repositoryName);
            var repositoryPath = new RepositoryPath(repository);
            this.PhysicalPath = Path.Combine(repositoryPath.PhysicalPath, DirName);
            this.VirtualPath = UrlUtility.Combine(repositoryPath.VirtualPath, DirName);
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
            get { throw new NotImplementedException(); }
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public void Rename(string newName)
        {
            throw new NotImplementedException();
        }

        #endregion 
    }
}