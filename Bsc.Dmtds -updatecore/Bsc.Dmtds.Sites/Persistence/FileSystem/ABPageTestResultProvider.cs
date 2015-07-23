using System.IO;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.ABTest;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.FileSystem
{
    [Bsc.Dmtds.Core.Runtime.Dependency.Dependency(typeof(IABPageTestResultProvider))]
    [Bsc.Dmtds.Core.Runtime.Dependency.Dependency(typeof(IProvider<ABPageTestResult>))]
    public class ABPageTestResultProvider : ObjectFileProviderBase<ABPageTestResult>, IABPageTestResultProvider
    {
        #region .ctor
        const string DIRNAME = "HitResults";
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        public ABPageTestResultProvider()
        {
        }
        #endregion

        #region CreateObject
        protected override ABPageTestResult CreateObject(Models.Site site, System.IO.FileInfo fileInfo)
        {
            return new ABPageTestResult() { Site = site, UUID = Path.GetFileNameWithoutExtension(fileInfo.Name) };
        }
        #endregion

        #region GetBasePath
        protected override string GetBasePath(Models.Site site)
        {
            var basePath = Path.Combine(site.PhysicalPath, ABPageSettingProvider.DIRNAME, DIRNAME);

            return basePath;
        }
        #endregion

        #region GetLocker
        protected override System.Threading.ReaderWriterLockSlim GetLocker()
        {
            return locker;
        }
        #endregion

        #region ISiteElementProvider InitializeToDB/ExportToDisk
        public void InitializeToDB(Site site)
        {
            //not need to implement.
        }

        public void ExportToDisk(Site site)
        {
            //not need to implement.
        }
        #endregion
    }
}