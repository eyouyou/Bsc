namespace Bsc.Dmtds.Caching
{
    public interface INotifyCacheExpired
    {
        /// <summary>
        /// Notifies the specified object cache name.
        /// </summary>
        /// <param name="objectCacheName">Name of the object cache.</param>
        /// <param name="key">The key.</param>
        void Notify(string objectCacheName, string key); 
    }
}