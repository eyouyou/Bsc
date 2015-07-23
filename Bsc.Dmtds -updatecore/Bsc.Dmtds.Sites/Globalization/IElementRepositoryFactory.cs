using Bsc.Dmtds.Common.Globalization;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Globalization
{
    public interface IElementRepositoryFactory
    {
        IElementRepository CreateRepository(Site site);
 
    }
    [Dependency(typeof(IElementRepositoryFactory))]
    public class DefaultElementRepositoryFactory : IElementRepositoryFactory
    {
        public IElementRepository CreateRepository(Site site)
        {
            return new SiteLabelRepository(site);
        }
    }
}