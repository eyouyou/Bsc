using System.Collections.Generic;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ISiteElementProvider<T> : IProvider<T>, ISiteExportableProvider
    {
        IEnumerable<T> All(Site site);
    }
}