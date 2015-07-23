using Bsc.Dmtds.Content.Persistence.Default;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Persistence.Caching
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private class ResolvingObserver : IResolvingObserver
        {
            public int Order
            {
                get { return 1; }
            }

            public object OnResolved(object resolvedObject)
            {
                if (resolvedObject is IMediaFolderProvider)
                {
                    return new MediaFolderProvider((IMediaFolderProvider)resolvedObject);
                }
                if (resolvedObject is IRepositoryProvider)
                {
                    return new RepositoryProvider((IRepositoryProvider)resolvedObject);
                }
                if (resolvedObject is ISchemaProvider)
                {
                    return new SchemaProvider((ISchemaProvider)resolvedObject);
                }
                if (resolvedObject is ITextFolderProvider)
                {
                    return new TextFolderProvider((ITextFolderProvider)resolvedObject);
                }

                return resolvedObject;
            }
        }

        public void Register(IContainerManager containerManager, ITypeFinder typeFinder)
        {
            containerManager.AddResolvingObserver(new ResolvingObserver());
        }

        public int Order
        {
            get { return 1; }
        }
    }
}