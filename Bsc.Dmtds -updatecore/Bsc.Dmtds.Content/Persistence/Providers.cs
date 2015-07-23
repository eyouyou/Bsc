using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Content.Persistence
{
    public static class Providers
    {
        public static IProviderFactory DefaultProviderFactory
        {
            get
            {
                return EngineContext.Current.Resolve<IProviderFactory>();
            }
            set
            {
                EngineContext.Current.ContainerManager.AddComponentInstance<IProviderFactory>(value);
            }
        }

        public static IRepositoryProvider RepositoryProvider
        {
            get
            {
                return DefaultProviderFactory.GetProvider<IRepositoryProvider>();
            }
        }
    }
}