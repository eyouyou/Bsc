using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Persistence.Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.DataRule;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Versioning;

namespace Bsc.Dmtds.Sites.Persistence.FileSystem
{
    [Dependency(typeof(IPageProvider))]
    [Dependency(typeof(IProvider<Page>))]
    public class PageProvider : InheritableProviderBase<Page>, IPageProvider, ILocalizableProvider<Page>
    {
        #region Versioning
        public class PageVersionLogger : FileVersionLogger<Page>
        {
            static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
            public override void LogVersion(Page o)
            {
                locker.EnterWriteLock();
                try
                {
                    VersionPath versionPath = new VersionPath(o, NextVersionId(o));
                    IOUtility.EnsureDirectoryExists(versionPath.PhysicalPath);
                    var versionDataFile = Path.Combine(versionPath.PhysicalPath, o.DataFileName);
                    PageProvider provider = new PageProvider();
                    provider.Serialize(o, versionDataFile);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }

            public override Page GetVersion(Page o, int version)
            {
                VersionPath versionPath = new VersionPath(o, version);
                var versionDataFile = Path.Combine(versionPath.PhysicalPath, o.DataFileName);
                Page page = null;
                if (File.Exists(versionDataFile))
                {
                    PageProvider provider = new PageProvider();
                    page = provider.Deserialize(o, versionDataFile);
                    ((IPersistable)page).Init(o);
                }
                return page;
            }

            public override void Revert(Page o, int version, string userName)
            {
                var versionData = GetVersion(o, version);
                if (versionData != null)
                {
                    versionData.UserName = userName;
                    versionData.LastUpdateDate = DateTime.UtcNow;
                    Providers.PageProvider.Update(versionData, o);
                    //log a new version when revert
                    LogVersion(versionData);
                }
            }
        }
        #endregion

        #region GetLocker
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        protected override System.Threading.ReaderWriterLockSlim GetLocker()
        {
            return locker;
        }
        #endregion

        #region KnownTypes
        protected override IEnumerable<Type> KnownTypes
        {
            get
            {
                return new Type[]{
                typeof(PagePosition),
                typeof(ViewPosition),
                typeof(ModulePosition),
                typeof(HtmlPosition),
                typeof(ContentPosition),
                typeof(HtmlBlockPosition),
                typeof(ProxyPosition),
                typeof(DataRuleBase),
                typeof(FolderDataRule),
                typeof(SchemaDataRule),
                typeof(CategoryDataRule),
                typeof(HttpDataRule)
                };
            }
        }
        #endregion

        #region IPageRepository Members

        public IEnumerable<Models.Page> ChildPages(Models.Page parentPage)
        {
            return ChildPagesEnumerable(parentPage).AsQueryable();
        }
        public IEnumerable<Models.Page> ChildPagesEnumerable(Models.Page parentPage)
        {
            List<Page> list = new List<Page>();
            if (parentPage.Exists())
            {
                string baseDir = parentPage.PhysicalPath;

                foreach (var dir in IOUtility.EnumerateDirectoriesExludeHidden(baseDir).Where(it => !it.Name.EqualsOrNullEmpty("~versions", StringComparison.OrdinalIgnoreCase)))
                {
                    var page = ModelActivatorFactory<Page>.GetActivator().Create(dir.FullName);
                    if (File.Exists(page.DataFile))
                    {
                        list.Add(page);
                    }
                }
            }
            return list;
        }

        #endregion

        #region Localize & Move Menbers
        public void Localize(Page sourcePage, Site targetSite)
        {
            var sourceSite = sourcePage.Site;
            if (sourceSite != targetSite)
            {
                var namePaths = sourcePage.PageNamePaths.ToArray();
                var destPage = new Page(targetSite, namePaths);
                var destPath = destPage.PhysicalPath;
                ILocalizableHelper.CopyFiles(sourcePage.PhysicalPath, destPath);
            }
        }
        public void Move(Site site, string pageFullName, string newParent)
        {
            GetLocker().EnterWriteLock();

            try
            {
                var page = PageHelper.Parse(site, pageFullName);
                Page parentPage = null;
                if (!string.IsNullOrEmpty(newParent))
                {
                    parentPage = PageHelper.Parse(site, newParent);
                    if (parentPage == page.Parent || parentPage == page)
                    {
                        throw new BscException(string.Format("The page is under '{0}' already", newParent));
                    }
                }
                Page newPage = null;
                if (parentPage != null)
                {
                    newPage = new Page(parentPage, page.Name);
                }
                else
                {
                    newPage = new Page(site, page.Name);
                }
                if (newPage.Exists())
                {
                    throw new BscException(string.Format("The page '{0}' already exists in '{1}'", page.Name, parentPage.FriendlyName));
                }
                Directory.Move(page.PhysicalPath, newPage.PhysicalPath);
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }
        }
        #endregion

        #region IPageRepository Relation Members

        private IEnumerable<Page> AllPagesNested(Site site)
        {
            return Providers.PageProvider.All(site).SelectMany(it => AllPagesNested(it));
        }
        private IEnumerable<Page> AllPagesNested(Page parent)
        {
            var childPages = Providers.PageProvider.ChildPages(parent);
            return new[] { parent }.Concat(childPages.SelectMany(it => AllPagesNested(it)));
        }
        public IEnumerable<Page> ByLayout(Layout layout)
        {
            return AllPagesNested(layout.Site)
                .Select(it => it.AsActual())
                .Where(it => it.Layout.EqualsOrNullEmpty(layout.Name, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();
        }

        public IEnumerable<Page> ByView(Models.View view)
        {
            return AllPagesNested(view.Site).Select(it => it.AsActual())
                .Where(it => it.PagePositions != null &&
                    it.PagePositions.Any(p => p is ViewPosition && ((ViewPosition)p).ViewName.EqualsOrNullEmpty(view.Name, StringComparison.CurrentCultureIgnoreCase)))
                    .ToArray();
        }

        public IEnumerable<Page> ByModule(Site site, string moduleName)
        {
            return AllPagesNested(site).Select(it => it.AsActual())
                 .Where(it => it.PagePositions != null &&
                     it.PagePositions.Any(p => p is ModulePosition && ((ModulePosition)p).ModuleName.EqualsOrNullEmpty(moduleName, StringComparison.CurrentCultureIgnoreCase)))
                     .ToArray();
        }
        public IEnumerable<Page> ByHtmlBlock(HtmlBlock htmlBlock)
        {
            return AllPagesNested(htmlBlock.Site).Select(it => it.AsActual())
               .Where(it => it.PagePositions != null &&
                   it.PagePositions.Any(p => p is HtmlBlockPosition && ((HtmlBlockPosition)p).BlockName.EqualsOrNullEmpty(htmlBlock.Name, StringComparison.CurrentCultureIgnoreCase)))
                   .ToArray();
        }
        #endregion

        #region Copy
        public Page Copy(Site site, string sourcePageFullName, string newPageFullName)
        {
            GetLocker().EnterWriteLock();

            try
            {
                var page = PageHelper.Parse(site, sourcePageFullName);
                var newPage = PageHelper.Parse(site, newPageFullName);

                IOUtility.CopyDirectory(page.PhysicalPath, newPage.PhysicalPath);

                return newPage;
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }
        }

        #endregion

        #region Draft
        public Page GetDraft(Page page)
        {
            var draftDataFile = GetDraftDataFile(page);
            Page draft = null;
            if (File.Exists(draftDataFile))
            {
                draft = Deserialize(page, draftDataFile);
                ((IPersistable)draft).Init(page);
            }
            return draft;
        }
        private static string GetDraftDataFile(Page page)
        {
            return page.DataFile + ".draft";
        }
        public void SaveAsDraft(Page page)
        {
            Serialize(page, GetDraftDataFile(page));
        }


        public void RemoveDraft(Page page)
        {
            var draftDataFile = GetDraftDataFile(page);
            GetLocker().EnterWriteLock();
            try
            {
                if (File.Exists(draftDataFile))
                {
                    File.Delete(draftDataFile);
                }
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }
        }
        #endregion

        #region Export
        public void Export(IEnumerable<Page> sources, Stream outputStream)
        {
            ImportHelper.Export(sources.OfType<PathResource>(), outputStream);
        }

        public void Import(Site site, Page parent, Stream zipStream, bool @override)
        {
            GetLocker().EnterWriteLock();
            try
            {
                var destDir = "";
                if (parent != null)
                {
                    destDir = parent.PhysicalPath;
                }
                else
                {
                    destDir = new Page(site, "Dummy").BasePhysicalPath;
                }
                ImportHelper.Import(site, destDir, zipStream, @override);
            }
            finally
            {
                GetLocker().ExitWriteLock();
            }
        }
        #endregion

        #region InitializePages

        public void InitializeToDB(Site site)
        {
            // no need to implement.
        }

        public void ExportToDisk(Site site)
        {
            // no need to implement.
        }


        public void Clear(Site site)
        {
            // no need to implement.
        }
        #endregion


    }
}