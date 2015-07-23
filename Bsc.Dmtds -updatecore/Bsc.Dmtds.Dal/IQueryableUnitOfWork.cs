using System.Data.Entity;
using Bsc.Dmtds.Core;

namespace Bsc.Dmtds.Dal
{
    public interface IQueryableUnitOfWork:ISql,IUnitOfWork
    {
        DbSet<T> CreateSet<T>() where T : class;

        void Attach<T>(T item) where T : class;

        void SetModified<T>(T item) where T : class;

        void ApplyCurrentValues<T>(T original, T current) where T : class;

    }
}
