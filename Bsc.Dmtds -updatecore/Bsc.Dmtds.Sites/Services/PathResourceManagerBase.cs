using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;

namespace Bsc.Dmtds.Sites.Services
{
    public abstract class PathResourceManagerBase<T, TProvider> : ManagerBase<T, TProvider>
        where T : PathResource, ISiteObject, IPersistable, IIdentifiable
        where TProvider : ISiteElementProvider<T>
    {
        #region .ctor
        public PathResourceManagerBase(TProvider provider)
            : base(provider)
        {

        }

        #endregion

        #region All
        public override IEnumerable<T> All(Site site, string filterName)
        {
            var r = Provider.All(site);
            if (!string.IsNullOrEmpty(filterName))
            {
                r = r.Where(it => it.Name.Contains(filterName, StringComparison.CurrentCultureIgnoreCase));
            }

            return r;
        }
        #endregion

        #region Update
        public override void Update(Site site, T @new, T @old)
        {
            if (string.IsNullOrEmpty(@new.Name))
            {
                throw new NameIsReqiredException();
            }

            old.Site = site;
            @new.Site = site;

            if (!old.Exists())
            {
                throw new ItemDoesNotExistException();
            }

            @new.LastUpdateDate = DateTime.UtcNow;

            Provider.Update(@new, @old);
        }
        #endregion

        #region Add
        public override void Add(Site site, T o)
        {
            if (string.IsNullOrEmpty(o.Name))
            {
                throw new NameIsReqiredException();
            }

            o.Site = site;
            if (o.Exists())
            {
                throw new ItemAlreadyExistsException();
            }

            o.LastUpdateDate = DateTime.UtcNow;

            Provider.Add(o);
        }
        #endregion
    }
}