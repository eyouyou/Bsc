using System.Collections.Generic;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.ABTest;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface IABSiteSettingProvider : IProvider<ABSiteSetting>
    {
        void Export(IEnumerable<ABSiteSetting> sources, System.IO.Stream outputStream);

        void Import(System.IO.Stream zipStream, bool @override);
    }
}