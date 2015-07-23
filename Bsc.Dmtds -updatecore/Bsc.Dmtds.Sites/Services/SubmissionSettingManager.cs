using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;

namespace Bsc.Dmtds.Sites.Services
{
    public class SubmissionSettingManager : ManagerBase<SubmissionSetting, ISubmissionSettingProvider>
    {
        #region .ctor
        ISubmissionSettingProvider _provider;
        public SubmissionSettingManager(ISubmissionSettingProvider provider)
            : base(provider)
        {
            _provider = provider;
        }
        #endregion

        #region All
        public override IEnumerable<SubmissionSetting> All(Models.Site site, string filterName)
        {
            var list = _provider.All(site);
            if (!string.IsNullOrEmpty(filterName))
            {
                list = list.Where(it => it.Name.Contains(filterName));
            }
            return list;
        }

        #endregion

        #region Get
        public override SubmissionSetting Get(Models.Site site, string name)
        {
            SubmissionSetting submission = null;
            while (submission == null && site != null)
            {
                submission = _provider.Get(new SubmissionSetting() { Site = site, Name = name });
                if (submission == null)
                {
                    site = site.Parent;
                }
            }
            return submission;
        }
        #endregion

        #region Update
        public override void Update(Models.Site site, SubmissionSetting @new, SubmissionSetting old)
        {
            @new.Site = site;
            @old.Site = site;
            _provider.Update(@new, old);
        }
        #endregion

        #region Import/export
        public virtual void Import(Site site, Stream zipStream, bool @override)
        {
            Provider.Import(site, zipStream, @override);
        }
        public virtual void Export(IEnumerable<SubmissionSetting> submissionSettings, System.IO.Stream outputStream)
        {
            Provider.Export(submissionSettings, outputStream);
        }
        public virtual void ExportAll(Site site, System.IO.Stream outputStream)
        {
            Provider.Export(All(site, ""), outputStream);
        }
        #endregion
    }
}