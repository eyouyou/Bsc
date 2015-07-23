using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core.Mvc.Paging;

namespace Bsc.Dmtds.Search
{
    public static class SearchHelper
    {
        static SearchHelper()
        {
            ServiceBuilder = new ServiceBuilder();
        }
        public static IServiceBuilder ServiceBuilder { get; set; }

        public static ISearchService OpenService(Repository repository)
        {
            return ServiceBuilder.OpenService(repository);
        }

        public static PagedList<Models.ResultObject> Search(this Repository repository, string key, int pageIndex, int pageSize, params string[] folders)
        {
            return OpenService(repository).Search(key, pageIndex, pageSize, folders);
        }
    }
}