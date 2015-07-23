using System.Collections.Generic;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Services
{
    public interface IManager<T>
    {
        IEnumerable<T> All(Site site, string filterName);

        T Get(Site site, string uuid);

        void Update(Site site, T @new, T @old);

        void Add(Site site, T item);

        void Remove(Site site, T item);
    }
}