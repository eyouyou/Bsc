using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ILabelProvider : ISiteElementProvider<Label>
    {
        IEnumerable<string> GetCategories(Site site);

        IQueryable<Label> GetLabels(Site site, string category);

        void AddCategory(Site site, string category);

        void RemoveCategory(Site site, string category);

        void Export(Site site, IEnumerable<Label> labels, IEnumerable<string> categories, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);

        void Flush(Site site);
    }
}