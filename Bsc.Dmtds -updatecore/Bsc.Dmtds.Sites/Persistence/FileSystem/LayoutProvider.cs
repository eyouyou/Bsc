using System;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.FileSystem
{
    [Dependency(typeof(ILayoutProvider))]
    [Dependency(typeof(IProvider<Layout>))]
    public class LayoutProvider : TemplateProvider<Layout>, ILayoutProvider
    {
        public class LayoutVersionLogger : TemplateProvider<Layout>.TemplateVersionLogger
        {
            static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
            protected override TemplateProvider<Layout> GetTemplateProvider()
            {
                return new LayoutProvider();
            }
            public override void Revert(Layout o, int version, string userName)
            {
                var versionData = GetVersion(o, version);
                if (versionData != null)
                {
                    versionData.UserName = userName;
                    versionData.LastUpdateDate = DateTime.UtcNow;
                    Providers.LayoutProvider.Update(versionData, o);
                    //log a new version when revert
                    LogVersion(versionData);
                }
            }

            protected override System.Threading.ReaderWriterLockSlim GetLocker()
            {
                return locker;
            }
        }
        static LayoutProvider()
        {

        }
        #region Layout

        public void Localize(Layout o, Site targetSite)
        {
            ILocalizableHelper.Localize<Layout>(o, targetSite);
        }
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        protected override System.Threading.ReaderWriterLockSlim GetLocker()
        {
            return locker;
        }
        #endregion



        public Layout Copy(Site site, string sourceName, string destName)
        {
            GetLocker().EnterWriteLock();

            try
            {
                var sourceLayout = new Layout(site, sourceName).LastVersion();
                var destLayout = new Layout(site, destName);

                IOUtility.CopyDirectory(sourceLayout.PhysicalPath, destLayout.PhysicalPath);

                return destLayout;
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }
        }
    }
}