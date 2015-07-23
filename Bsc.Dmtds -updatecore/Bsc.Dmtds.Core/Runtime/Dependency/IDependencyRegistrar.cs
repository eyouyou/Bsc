namespace Bsc.Dmtds.Core.Runtime.Dependency
{
    public interface IDependencyRegistrar
    {
        void Register(IContainerManager containerManager, ITypeFinder typeFinder);

        int Order { get; } 
    }
}