using System.Collections.Generic;
using Bsc.Dmtds.Sites.ABTest;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface IABRuleSettingProvider:ISiteElementProvider<ABRuleSetting>
    {
        void Export(IEnumerable<ABRuleSetting> sources, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);
    }
}