using System;
using System.IO;

namespace Bsc.Dmtds.Content.Models.Paths
{
    public class BroadcastingPath : IPath
    {
        static string DIR = "Broadcasting";

        public BroadcastingPath(Repository repository)
        {
            var repositoryPath = new RepositoryPath(repository);
            this.PhysicalPath = Path.Combine(repositoryPath.PhysicalPath, DIR);
        }
        public string PhysicalPath
        {
            get;
            private set;

        }

        public string VirtualPath
        {
            get { throw new NotImplementedException(); }
        }

        public string SettingFile
        {
            get { throw new NotImplementedException(); }
        }

        public bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }

        public void Rename(string newName)
        {
            throw new NotImplementedException();
        }
    }
}