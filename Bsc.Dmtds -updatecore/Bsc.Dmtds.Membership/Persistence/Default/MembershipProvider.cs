using System.Collections.Generic;
using System.IO;
using System.Text;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Ionic.Zip;

namespace Bsc.Dmtds.Membership.Persistence.Default
{
    [Bsc.Dmtds.Core.Runtime.Dependency.Dependency(typeof(IMembershipProvider))]
    [Bsc.Dmtds.Core.Runtime.Dependency.Dependency(typeof(IProvider<Bsc.Dmtds.Membership.Models.Membership>))]

    public class MembershipProvider : SettingFileProviderBase<Bsc.Dmtds.Membership.Models.Membership>, IMembershipProvider
    {
        #region .ctor
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        protected static string Dir_Name = "Members";
        MembershipPath _membershipPath = null;
        public MembershipProvider(MembershipPath membershipPath)
        {
            this._membershipPath = membershipPath;
        }
        #endregion

        #region All
        public IEnumerable<Bsc.Dmtds.Membership.Models.Membership> All()
        {
            string baseDir = GetBasePath();
            List<Bsc.Dmtds.Membership.Models.Membership> list = new List<Bsc.Dmtds.Membership.Models.Membership>();
            if (Directory.Exists(baseDir))
            {
                foreach (var dir in IOUtility.EnumerateDirectoriesExludeHidden(baseDir))
                {
                    if (File.Exists(Path.Combine(dir.FullName, _membershipPath.BaseDir.SettingFileName)))
                    {
                        list.Add(new Bsc.Dmtds.Membership.Models.Membership() { Name = dir.Name });
                    }
                }
            }
            return list;
        }
        #endregion

        #region  abstract implementation
        protected override System.Threading.ReaderWriterLockSlim GetLocker()
        {
            return locker;
        }
        protected virtual string GetBasePath()
        {
            return _membershipPath.GetBasePath();
        }
        protected override string GetDataFilePath(Bsc.Dmtds.Membership.Models.Membership o)
        {
            return _membershipPath.GetMembershipSettingFilePath(o);
        }
        #endregion

        #region Export
        public Bsc.Dmtds.Membership.Models.Membership Import(string membershipName, Stream stream)
        {
            var membership = new Bsc.Dmtds.Membership.Models.Membership(membershipName);
            if (Get(membership) != null)
            {
                return membership;
            }
            var path = _membershipPath.GetMembershipPath(membership);
            using (ZipFile zipFile = ZipFile.Read(stream))
            {
                ExtractExistingFileAction action = ExtractExistingFileAction.OverwriteSilently;
                zipFile.ExtractAll(path, action);
            }
            return membership;
        }
        public void Export(Bsc.Dmtds.Membership.Models.Membership membership, Stream outputStream)
        {
            var physicalPath = _membershipPath.GetMembershipPath(membership);
            using (ZipFile zipFile = new ZipFile(Encoding.UTF8))
            {
                //zipFile.ZipError += new EventHandler<ZipErrorEventArgs>(zipFile_ZipError);

                zipFile.ZipErrorAction = ZipErrorAction.Skip;


                zipFile.AddSelectedFiles("name != *\\~versions\\*.* and name != *\\.svn\\*.* and name != *\\_svn\\*.*", physicalPath, "", true);

                zipFile.Save(outputStream);
            }
        }
        #endregion
        #region MembershipPath
        public class MembershipPath
        {
            protected static string Dir_Name = "Memberships";
            public IBaseDir BaseDir { get; private set; }
            public MembershipPath(IBaseDir baseDir)
            {
                this.BaseDir = baseDir;
            }

            public virtual string GetBasePath()
            {
                return Path.Combine(BaseDir.DataPhysicalPath, Dir_Name);
            }

            public virtual string GetMembershipPath(Bsc.Dmtds.Membership.Models.Membership o)
            {
                var baseDir = GetBasePath();

                return Path.Combine(baseDir, o.Name);
            }

            public virtual string GetMembershipSettingFilePath(Bsc.Dmtds.Membership.Models.Membership o)
            {
                var membershipPath = GetMembershipPath(o);

                return Path.Combine(membershipPath, BaseDir.SettingFileName);
            }
        }
        #endregion
    }
}