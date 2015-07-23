using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Persistence;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Services
{
    [Dependency(typeof(MediaFolderManager))]
    public class MediaFolderManager:FolderManager<MediaFolder>
    {
        public MediaFolderManager(IMediaFolderProvider provider)
            : base(provider) { }

        public virtual void Rename(MediaFolder @new, MediaFolder old)
        {
            if (string.IsNullOrEmpty(@new.Name))
            {
                throw new NameIsReqiredException();
            }
            ((IMediaFolderProvider)Provider).Rename(@new, @old);
        }
    }
}