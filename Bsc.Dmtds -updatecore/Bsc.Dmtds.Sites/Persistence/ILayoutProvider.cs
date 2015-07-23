using System.Collections.Generic;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ILayoutProvider : ISiteElementProvider<Layout>, ILocalizableProvider<Layout>
    {
        Models.Layout Copy(Site site, string sourceName, string destName);

        void Export(IEnumerable<Layout> sources, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);
    }
}