

namespace Bsc.Dmtds.Content
{
/*    public abstract class CacheRepositoryBase<T>
        where T : class,IPersistable
    {
        private IRepository<T> innerRepository;
        public CacheRepositoryBase(IRepository<T> inner)
        {
            this.innerRepository = inner;
        }

        public virtual T Get(T dummy)
        {
            var cacheKey = GetCacheKey(dummy);

            var cached = CacheManagerFactory.DefaultCacheManager.GlobalObjectCache().GetCache(cacheKey, () => innerRepository.Get(dummy));

            return cached;
        }

        protected abstract string GetCacheKey(T o);

        protected virtual void ClearObjectCache(T o)
        {
            var cacheKey = GetCacheKey(o);
            CacheManagerFactory.DefaultCacheManager.GlobalObjectCache().Remove(cacheKey);
        }

        public virtual void Add(T item)
        {
            innerRepository.Add(item);
            ClearObjectCache(item);
        }



        public virtual void Update(T @new, T old)
        {
            ClearObjectCache(@old);

            innerRepository.Update(@new, old);
        }

        public virtual void Remove(T item)
        {
            ClearObjectCache(item);
            innerRepository.Remove(item);
        }

    }*/
}