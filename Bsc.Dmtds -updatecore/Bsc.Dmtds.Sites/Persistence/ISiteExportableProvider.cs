
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ISiteExportableProvider
    {
        void InitializeToDB(Site site);

        void ExportToDisk(Site site); 
    }
}