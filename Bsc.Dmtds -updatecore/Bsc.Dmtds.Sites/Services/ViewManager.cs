﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Persistence;
using Bsc.Dmtds.Sites.Versioning;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(ViewManager))]
    public class ViewManager : PathResourceManagerBase<Models.View, IViewProvider>
    {
        #region .ctor
        public ViewManager(IViewProvider provider)
            : base(provider)
        {
        }
        #endregion

        #region GetNamespace
        public virtual Namespace<Models.View> GetNamespace(Site site, params string[] exculdes)
        {
            var views = All(site, "").Where(it => !exculdes.Any(ex => ex.EqualsOrNullEmpty(it.Name, StringComparison.OrdinalIgnoreCase)));

            Dictionary<string, Namespace<Models.View>> namespaces = new Dictionary<string, Namespace<Models.View>>(StringComparer.CurrentCultureIgnoreCase);
            Namespace<Models.View> root = new Namespace<Models.View>();
            foreach (var item in views)
            {
                string parentName = "";
                Namespace<Models.View> last = null;
                var names = item.Name.Split(".".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var name in names.Take(names.Length - 1))
                {
                    string fullName = "";
                    if (string.IsNullOrEmpty(parentName))
                    {
                        fullName = name;
                    }
                    else
                    {
                        fullName = parentName + "." + name;
                    }
                    if (!namespaces.ContainsKey(fullName))
                    {
                        last = new Namespace<Models.View>()
                        {
                            Name = name,
                            FullName = fullName
                        };
                        namespaces[fullName] = last;
                        if (!string.IsNullOrEmpty(parentName))
                        {
                            namespaces[parentName].AddNamespace(last);
                        }
                        else
                        {
                            root.AddNamespace(last);
                        }
                    }
                    else
                    {
                        last = namespaces[fullName];
                    }
                    parentName = fullName;
                }
                if (last == null)
                {
                    root.AddEntry(item.Name, item);
                }
                else
                {
                    last.AddEntry(item.Name, item);
                }
            }
            return root;
        }
        public virtual IEnumerable<Models.View> ByNamespace(Site site, string ns, string filter)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return All(site, filter).Where(it => !it.Name.Contains(".", StringComparison.CurrentCultureIgnoreCase));
            }
            else
                return All(site, filter).Where(it => it.Name.StartsWith(ns + ".", StringComparison.CurrentCultureIgnoreCase)
                    && it.Name.Split(".".ToArray(), StringSplitOptions.RemoveEmptyEntries).Length == ns.Split(".".ToArray(), StringSplitOptions.RemoveEmptyEntries).Length + 1);
        }
        #endregion

        #region Add
        public override void Add(Site site, Models.View o)
        {
            HasSameName(site, o.Name);

            base.Add(site, o);

            VersionManager.LogVersion(o);

        }
        private bool HasSameName(Site site, string name)
        {
            if (site.Parent != null)
            {
                var query = Get(site.Parent, name);
                if (query != null)
                { throw new ItemAlreadyExistsException(); }
                else
                {
                    return HasSameName(site.Parent, name);
                }
            }
            return false;
        }
        #endregion

        #region Get
        public override Models.View Get(Models.Site site, string name)
        {
            return Provider.Get(new Models.View(site, name));
        }
        #endregion

        #region Update
        public override void Update(Site site, Models.View @new, Models.View old)
        {
            base.Update(site, @new, old);

            VersionManager.LogVersion(@new);
        }
        #endregion

        #region Remove

        protected override void CheckIsBeingUsed(Models.View item)
        {
            if (!item.HasParentVersion() && Relations(item).Count() > 0)
            {
                throw new BscException(string.Format("'{0}' is being used.", item.Name));
            }
        }
        #endregion

        #region Localize
        public virtual void Localize(string name, Site targetSite, string userName = null)
        {
            var target = new Models.View(targetSite, name);
            var source = target.LastVersion();
            if (target.Site != source.Site)
            {
                ((IViewProvider)Provider).Localize(source, targetSite);
                target = target.AsActual();
                if (target != null)
                {
                    target.UserName = userName;
                    Update(targetSite, target, target);
                }
            }

        }
        #endregion

        #region Relations
        public override IEnumerable<RelationModel> Relations(Models.View view)
        {
            view = view.AsActual();
            var pageRepository = (IPageProvider)Providers.ProviderFactory.GetProvider<IProvider<Page>>();
            return pageRepository.ByView(view).Select(it => new RelationModel()
            {
                DisplayName = it.FriendlyName,
                ObjectUUID = it.FullName,
                RelationObject = it,
                RelationType = "Page"
            });

        }

        #endregion

        #region Copy

        public virtual void Copy(Site site, string sourceName, string destName)
        {
            ((IViewProvider)Provider).Copy(site, sourceName, destName);
        }

        #endregion
    }
}