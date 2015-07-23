using System.Collections.Generic;

namespace Bsc.Dmtds.Core.Persistence.Non_Relational
{
    public interface IProvider<T>
    {
        IEnumerable<T> All();
        T Get(T dummy);
        void Add(T item);
        void Update(T @new, T old);
        void Remove(T item);
    }
}