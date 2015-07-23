using System;

namespace Bsc.Dmtds.Core
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();

        void CommitAndRefreshChanges();

        void RollbackChanges();

        //T GetDbContext<T>() where T : class;
    }
}
