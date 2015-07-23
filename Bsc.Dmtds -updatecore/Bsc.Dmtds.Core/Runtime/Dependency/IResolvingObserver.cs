namespace Bsc.Dmtds.Core.Runtime.Dependency
{
    public interface IResolvingObserver
    {
        int Order { get; }
        object OnResolved(object resolvedObject); 
    }
}