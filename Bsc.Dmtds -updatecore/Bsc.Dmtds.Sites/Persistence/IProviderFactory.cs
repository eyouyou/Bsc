namespace Bsc.Dmtds.Sites.Persistence
{
    public interface IProviderFactory
    {
        T GetProvider<T>()
            where T : class;

        void RegisterProvider<ServiceType>(ServiceType provider)
            where ServiceType : class; 
    }
}