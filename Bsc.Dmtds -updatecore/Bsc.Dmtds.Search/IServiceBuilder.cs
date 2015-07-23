using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Search
{
    public interface IServiceBuilder
    {
        ISearchService OpenService(Repository repository);
 
    }
    public class ServiceBuilder : IServiceBuilder
    {
        public ISearchService OpenService(Repository repository)
        {
            return new SearchService(repository);
        }
    }
}