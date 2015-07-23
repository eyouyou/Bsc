using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Caching;
using Bsc.Dmtds.Content.Caching;
using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Persistence.Caching
{
    public class TextFolderProvider : CacheProviderBase<TextFolder>, ITextFolderProvider
    {
        #region .ctor
        private ITextFolderProvider inner;
        public TextFolderProvider(ITextFolderProvider innerProvider)
            : base(innerProvider)
        {
            inner = innerProvider;
        }
        #endregion

        #region BySchema
        public IQueryable<TextFolder> BySchema(Schema schema)
        {
            return inner.BySchema(schema);
        }

        #endregion

        #region ChildFolders
        public IQueryable<TextFolder> ChildFolders(TextFolder parent)
        {
            return parent.Repository.ObjectCache().GetCache<TextFolder[]>("TextFolderProvider.ChildFolders:Parent:" + parent.FullName.ToLower(), () =>
            {
                return inner.ChildFolders(parent).ToArray();
            }).AsQueryable();
        }
        #endregion

        #region All
        public IEnumerable<TextFolder> All(Repository repository)
        {
            return repository.ObjectCache().GetCache<TextFolder[]>("TextFolderProvider.All", () =>
            {
                return inner.All(repository).ToArray();
            }).AsQueryable();
        }
        #endregion

        #region Export
        public void Export(Repository repository, IEnumerable<TextFolder> models, System.IO.Stream outputStream)
        {
            inner.Export(repository, models, outputStream);
        }
        #endregion

        #region Import
        public void Import(Repository repository, TextFolder folder, System.IO.Stream zipStream, bool @override)
        {
            try
            {
                inner.Import(repository, folder, zipStream, @override);
            }
            finally
            {
                repository.ClearCache();
            }
        }
        #endregion

        #region GetCacheKey
        protected override string GetCacheKey(TextFolder o)
        {
            return "TextFolder:" + o.FullName.ToLower();
        }
        #endregion

        #region All
        public IEnumerable<TextFolder> All()
        {
            return inner.All();
        }
        #endregion
    }
}