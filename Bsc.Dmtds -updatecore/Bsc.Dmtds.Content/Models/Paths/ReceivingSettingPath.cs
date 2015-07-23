using System;
using System.IO;

namespace Bsc.Dmtds.Content.Models.Paths
{
    public class ReceivingSettingPath:IPath
    {
        private static string DIR = "ReceivingSettings";
        public ReceivingSettingPath(Repository repository)
        {
            this.PhysicalPath = Path.Combine(new BroadcastingPath(repository).PhysicalPath, DIR);

            Bsc.Dmtds.Common.IO.IOUtility.EnsureDirectoryExists(PhysicalPath);
        }
        public ReceivingSettingPath(ReceivingSetting receiveSetting)
        {
            var fileName = receiveSetting.Name + ".config";
            this.SettingFile = this.PhysicalPath = Path.Combine(new BroadcastingPath(receiveSetting.Repository).PhysicalPath, DIR, fileName);
            //this.VirtualPath = UrlUtility.Combine(new BroadcastingPath().PhysicalPath, DIR, fileName);
        }

        #region IPath Members

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
            get;
            private set;
        }

        public bool Exists()
        {
            return File.Exists(this.SettingFile);
        }

        public void Rename(string newName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}