using System.Collections.Generic;
using Bsc.Dmtds.Sites.ABTest;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface IABPageSettingProvider : ISiteElementProvider<ABPageSetting>
    {
        void Export(IEnumerable<ABPageSetting> sources, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);

    }
}