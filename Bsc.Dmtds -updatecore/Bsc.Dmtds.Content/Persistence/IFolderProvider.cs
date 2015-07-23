using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Persistence
{
    public interface IFolderProvider<T>:IContentElementProvider<T>
        where T : Folder

    {
        IQueryable<T> ChildFolders(T parent);
        void Export(Repository repository, IEnumerable<T> models, System.IO.Stream outputStream);
        void Import(Repository repository, T folder, System.IO.Stream zipStream, bool @override);

    }
    public interface ITextFolderProvider : IFolderProvider<TextFolder>
    {
        IQueryable<TextFolder> BySchema(Schema schema);
    }
    public interface IMediaFolderProvider : IFolderProvider<MediaFolder>
    {
        void Rename(MediaFolder @new, MediaFolder old);
        void Export(Repository repository, string baseFolder, string[] folders, string[] docs, Stream outputStream);
    }
}