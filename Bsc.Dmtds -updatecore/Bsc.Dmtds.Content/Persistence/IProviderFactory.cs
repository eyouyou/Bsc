namespace Bsc.Dmtds.Content.Persistence
{
    public interface IProviderFactory
    {
        string Name { get; }

        T GetProvider<T>()
            where T : class;

        void RegisterProvider<ServiceType>(ServiceType provider)
            where ServiceType : class; 
    }
}