using System.Collections.Generic;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface IViewProvider : ISiteElementProvider<Sites.Models.View>, ILocalizableProvider<Sites.Models.View>
    {
        Models.View Copy(Site site, string sourceName, string destName);

        void Export(IEnumerable<Models.View> sources, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);
    }
}