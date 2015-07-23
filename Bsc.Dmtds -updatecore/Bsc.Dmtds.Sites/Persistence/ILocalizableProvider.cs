using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ILocalizableProvider<T>
    {
        void Localize(T o, Site targetSite);

    }
}