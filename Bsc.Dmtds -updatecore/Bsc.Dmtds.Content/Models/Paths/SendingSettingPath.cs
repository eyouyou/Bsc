using System;
using System.IO;

namespace Bsc.Dmtds.Content.Models.Paths
{
    public class SendingSettingPath: IPath
    {
        private static string DIR = "SendingSettings";
        public SendingSettingPath(Repository repository)
        {
            this.PhysicalPath = Path.Combine(new BroadcastingPath(repository).PhysicalPath, DIR);
            Bsc.Dmtds.Common.IO.IOUtility.EnsureDirectoryExists(PhysicalPath);
        }
        public SendingSettingPath(SendingSetting sendingSetting)
        {
            var fileName = sendingSetting.Name + ".config";
            this.SettingFile = this.PhysicalPath = Path.Combine(new BroadcastingPath(sendingSetting.Repository).PhysicalPath, DIR, fileName);
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