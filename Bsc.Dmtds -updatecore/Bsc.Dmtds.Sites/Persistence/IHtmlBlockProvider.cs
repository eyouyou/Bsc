using System.Collections.Generic;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface IHtmlBlockProvider : ISiteElementProvider<HtmlBlock>, ILocalizableProvider<HtmlBlock>
    {
        void Clear(Site site);

        void Export(IEnumerable<HtmlBlock> sources, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);
    }
}