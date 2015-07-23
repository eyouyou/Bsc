using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(UrlRedirectManager))]
    public class UrlRedirectManager : ManagerBase<UrlRedirect, IUrlRedirectProvider>
    {
        public UrlRedirectManager(IUrlRedirectProvider provider) : base(provider) { }

        #region Export & Import

        public virtual void Export(Site site, System.IO.Stream outputStream)
        {
            ((IUrlRedirectProvider)Provider).Export(site, outputStream);
        }

        public virtual void Import(Site site, System.IO.Stream zipStream, bool @override)
        {
            ((IUrlRedirectProvider)Provider).Import(site, zipStream, @override);
        }
        #endregion

        public override IEnumerable<UrlRedirect> All(Site site, string filterName)
        {
            var result = Provider.All(site);
            if (!string.IsNullOrEmpty(filterName))
            {
                result = result.Where(it => it.InputUrl.Contains(filterName, StringComparison.CurrentCultureIgnoreCase));
            }
            return result;
        }
        public virtual UrlRedirect GetByInputUrl(Site site, string inputUrl)
        {
            return Provider.All(site).Where(it => it.InputUrl.EqualsOrNullEmpty(inputUrl, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
        public override UrlRedirect Get(Site site, string name)
        {
            return Provider.Get(new UrlRedirect { Site = site, UUID = name });
        }

        public override void Update(Site site, UrlRedirect @new, UrlRedirect old)
        {
            @new.Site = site;
            old.Site = site;
            if (Get(site, old.UUID) == null)
            {
                throw new ItemDoesNotExistException();
            }
            Provider.Update(@new, old);
        }
    }
}