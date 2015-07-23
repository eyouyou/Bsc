using System.Collections.Generic;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence
{
    public interface ISubmissionSettingProvider:ISiteElementProvider<SubmissionSetting>
    {
        void Export(IEnumerable<SubmissionSetting> sources, System.IO.Stream outputStream);

        void Import(Site site, System.IO.Stream zipStream, bool @override);
    }
}