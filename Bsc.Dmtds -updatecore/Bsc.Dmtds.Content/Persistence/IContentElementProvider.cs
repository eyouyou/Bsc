using System.Collections.Generic;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Persistence.Relational;

namespace Bsc.Dmtds.Content.Persistence
{
    public interface IContentElementProvider<T> : IProvider<T>
    {
        IEnumerable<T> All(Repository repository);

    }
}