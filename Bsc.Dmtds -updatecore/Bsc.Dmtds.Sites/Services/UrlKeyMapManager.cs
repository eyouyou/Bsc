using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(UrlKeyMapManager))]
    public class UrlKeyMapManager : ManagerBase<UrlKeyMap, IUrlKeyMapProvider>
    {
        public UrlKeyMapManager(IUrlKeyMapProvider provider) : base(provider) { }

        #region Export & Import

        public void Export(Site site, System.IO.Stream outputStream)
        {
            Provider.Export(site, outputStream);
        }

        public void Import(Site site, System.IO.Stream zipStream, bool @override)
        {
            Provider.Import(site, zipStream, @override);
        }

        #endregion

        public override IEnumerable<UrlKeyMap> All(Site site, string filterName)
        {
            var result = Provider.All(site);
            if (!string.IsNullOrEmpty(filterName))
            {
                result = result.Where(it => it.Name.Contains(filterName, StringComparison.CurrentCultureIgnoreCase));
            }
            return result;
        }

        public override UrlKeyMap Get(Site site, string name)
        {
            return Provider.Get(new UrlKeyMap() { Site = site, Name = name });
        }

        public override void Update(Site site, UrlKeyMap @new, UrlKeyMap old)
        {
            @new.Site = site;
            old.Site = site;
            if (Get(site, old.Key.ToString()) == null)
            {
                throw new ItemDoesNotExistException();
            }
            if (!old.Equals(@new))
            {
                // the key has been changed...
                if (Get(site, @new.Key.ToString()) != null)
                {
                    throw new ItemAlreadyExistsException();
                }
            }
            Provider.Update(@new, old);
        }
    }
}