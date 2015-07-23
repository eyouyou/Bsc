using System;

namespace Bsc.Dmtds.Sites.Extension.Management
{
    public class InstallationContext
    {
        #region .ctor
        public InstallationContext()
        {

        }

        public InstallationContext(string moduleName, string targetVersion, DateTime utcDateTime)
        {
            this.ModuleName = moduleName;
            this.VersionRange = new VersionRange(targetVersion);
            this.Datetime = utcDateTime;
        }
        public InstallationContext(string moduleName, string sourceVersion, string targetVersion, DateTime utcDateTime)
        {
            this.ModuleName = moduleName;
            this.VersionRange = new VersionRange(sourceVersion, targetVersion);
            this.Datetime = utcDateTime;
        }
        #endregion
        public string ModuleName { get; set; }
        public string User { get; set; }
        public DateTime Datetime { get; set; }
        public VersionRange VersionRange { get; set; }

        public string InstallationFileName { get; set; } 
    }
}