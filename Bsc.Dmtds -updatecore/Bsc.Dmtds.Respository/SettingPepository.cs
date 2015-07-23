using System;
using System.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Dal.FileSystem;
using Bsc.Dmtds.Respository.IRepository;

namespace Bsc.Dmtds.Respository
{
    [Dependency(typeof(ISettingPepository))]
    [Dependency(typeof(IProvider<Setting>))]
    public class SettingPepository : ObjectFileRepository<Setting>, ISettingPepository
    {
        private IAccountBaseDir accountBaseDir;
        private static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();

        public SettingPepository(IAccountBaseDir baseDir)
        {
            accountBaseDir = baseDir;
        }
        protected override System.Threading.ReaderWriterLockSlim GetLocker()
        {
            return locker;
        }

        protected override string GetFilePath(Setting o)
        {
            return Path.Combine(accountBaseDir.PhysicalPath, "setting.config");
        }

        protected override string GetBasePath()
        {
            return accountBaseDir.PhysicalPath;
        }

        protected override Setting CreateObject(string filePath)
        {
            throw new NotImplementedException();
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public override void Update(Setting @new, Setting old)
        {
            string filePath = GetFilePath(@old);
            Save(@new, filePath);
        }
        public Setting Get()
        {
            var setting = Get(null);
            if (setting == null)
            {
                setting = new Setting()
                {
                    EnableLockout = true,
                    FailedPasswordAttemptCount = 5,
                    MinutesToUnlock = 15,
                    PasswordInvalidMessage = "该密码无效.",
                    PasswordStrength = ".{5,}"
                };
            }
            return setting;
        }

        public void Dispose()
        {
            
        }
    }
}