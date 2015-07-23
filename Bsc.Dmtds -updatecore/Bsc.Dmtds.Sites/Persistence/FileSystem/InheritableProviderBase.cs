using System.Collections.Generic;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.FileSystem
{
    public abstract class InheritableProviderBase<T> : SettingFileProviderBase<T>
        where T : PathResource, ISiteObject, IFilePersistable, IPersistable, IIdentifiable, IInheritable<T>
    {
        public override IEnumerable<T> All(Models.Site site)
        {
            return IInheritableHelper.All<T>(site);
        }

        public override void Update(T @new, T old)
        {
            if (!@new.Equals(old) && old.Exists())
            {
                old.Rename(@new.Name);
            }
            Save(@new);
        }
        public override void Remove(T item)
        {
            string dir = item.PhysicalPath;
            GetLocker().EnterWriteLock();
            try
            {

                IOUtility.DeleteDirectory(dir, true);

            }
            finally
            {
                GetLocker().ExitWriteLock();
            }
        }
    }
}