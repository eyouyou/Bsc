using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Dal.FileSystem
{
    public abstract class ObjectFileRepository<T>
        where T : IPersistable
    {
        protected abstract System.Threading.ReaderWriterLockSlim GetLocker();
        protected abstract string GetFilePath(T o);
        protected abstract string GetBasePath();
        protected abstract T CreateObject(string filePath);
        public virtual IEnumerable<T> All()
        {
            GetLocker().EnterReadLock();
            try
            {
                return AllEnumerable().Select(o => Get(o));
            }
            finally
            {
                GetLocker().ExitReadLock();
            }

        }
        private IEnumerable<T> AllEnumerable()
        {
            if (Directory.Exists(GetBasePath()))
            {
                foreach (var filePath in Directory.EnumerateFiles(GetBasePath(), "*.config"))
                {
                    //string fileName = Path.GetFileNameWithoutExtension(filePath);
                    yield return CreateObject(filePath);
                }
            }
        }
        public virtual T Get(T dummy)
        {
            string filePath = GetFilePath(dummy);
            if (File.Exists(filePath))
            {
                GetLocker().EnterWriteLock();
                try
                {
                    var item = Bsc.Dmtds.Common.Util.DataContractSerializationHelper.Deserialize<T>(filePath);
                    item.Init(dummy);
                    return item;
                }
                finally
                {
                    GetLocker().ExitWriteLock();
                }
            }
            return default(T);
        }

        public virtual void Add(T item)
        {
            string filePath = GetFilePath(item);
            if (File.Exists(filePath))
            {
                throw new BscException("该事物已存在");
            }
            Save(item, filePath);
        }

        protected void Save(T item, string filePath)
        {
            GetLocker().EnterWriteLock();
            try
            {
                item.OnSaving();
                Bsc.Dmtds.Common.Util.DataContractSerializationHelper.Serialize<T>(item, filePath);
                item.OnSaved();
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }

        }

        public virtual void Update(T @new, T old)
        {
            string filePath = GetFilePath(@old);
            if (!File.Exists(filePath))
            {
                throw new BscException("该事物不存在.");
            }
            Save(@new, filePath);
        }

        public virtual void Remove(T item)
        {
            string filePath = GetFilePath(item);
            if (File.Exists(filePath))
            {
                GetLocker().EnterWriteLock();
                try
                {
                    File.Delete(filePath);
                }
                finally
                {
                    GetLocker().ExitWriteLock();
                }

            }
        }
    }
}