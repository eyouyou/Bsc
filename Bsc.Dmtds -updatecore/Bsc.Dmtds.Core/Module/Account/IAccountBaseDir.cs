using System.IO;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Core.Module.Account
{
    public interface IAccountBaseDir
    {
        string PathName { get; }
        string PhysicalPath { get; } 
    }
    [Dependency(typeof(IAccountBaseDir))]
    public class AccountBaseDir : IAccountBaseDir
    {
        public AccountBaseDir(IBaseDir baseDir)
        {
            this.PathName = "Account";
            this.PhysicalPath = Path.Combine(baseDir.DataPhysicalPath, this.PathName);
        }
        public string PathName
        {
            get;
            private set;
        }

        public string PhysicalPath
        {
            get;
            private set;
        }
    }
}