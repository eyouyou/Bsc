using System;
using System.Collections.Generic;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.DataRule;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.FileSystem
{
    [Dependency(typeof(IViewProvider))]
    [Dependency(typeof(IProvider<Models.View>))]
    public class ViewProvider : TemplateProvider<Models.View>, IViewProvider
    {
        #region ViewVersionLogger
        public class ViewVersionLogger : TemplateProvider<Models.View>.TemplateVersionLogger
        {
            static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
            protected override TemplateProvider<Models.View> GetTemplateProvider()
            {
                return new ViewProvider();
            }
            public override void Revert(Models.View o, int version, string userName)
            {
                var versionData = GetVersion(o, version);
                if (versionData != null)
                {
                    versionData.UserName = userName;
                    versionData.LastUpdateDate = DateTime.UtcNow;
                    Providers.ViewProvider.Update(versionData, o);
                    //log a new version when revert
                    LogVersion(versionData);
                }
            }

            protected override System.Threading.ReaderWriterLockSlim GetLocker()
            {
                return locker;
            }
        }
        #endregion

        #region KnownTypes
        protected override IEnumerable<Type> KnownTypes
        {
            get
            {
                return new Type[]{
                typeof(DataRuleBase),
                typeof(FolderDataRule),
                typeof(SchemaDataRule),
                typeof(CategoryDataRule),
                typeof(HttpDataRule)
                };
            }
        }
        #endregion

        #region Localize
        public void Localize(Models.View o, Site targetSite)
        {
            ILocalizableHelper.Localize<Models.View>(o, targetSite);
        }
        #endregion

        #region GetLocker
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        protected override System.Threading.ReaderWriterLockSlim GetLocker()
        {
            return locker;
        }
        #endregion

        #region Copy
        public Models.View Copy(Site site, string sourceName, string destName)
        {
            GetLocker().EnterWriteLock();

            try
            {
                var sourceView = new Models.View(site, sourceName).LastVersion();
                var destView = new Models.View(site, destName);

                IOUtility.CopyDirectory(sourceView.PhysicalPath, destView.PhysicalPath);

                return destView;
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }

        }
        #endregion

        #region InitializeToDB/ExportToDisk
        public void InitializeToDB(Site site)
        {
            //
        }

        public void ExportToDisk(Site site)
        {
            //
        }
        #endregion
    }
}