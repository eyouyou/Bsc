using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Core.Persistence.Non_Relational
{
    public static class IPersistableExtensions
    {
        public static T AsActual<T>(this T persistable)
            where T : IPersistable
        {
            if (persistable == null)
            {
                return persistable;
            }
            if (persistable.IsDummy)
            {
                var provider = EngineContext.Current.Resolve<IProvider<T>>();
                return provider.Get(persistable);
            }
            return persistable;
        } 
    }
}