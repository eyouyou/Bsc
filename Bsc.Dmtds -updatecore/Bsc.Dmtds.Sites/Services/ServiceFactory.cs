using System.Collections.Generic;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Persistence;
using Bsc.Dmtds.Content.Services;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Services
{
    public class ContentWorkflowManager : WorkflowManager
    {
        public ContentWorkflowManager(IWorkflowProvider workflowProvider,
            IPendingWorkflowItemProvider pendingWorkflowItemProvider,
            IWorkflowHistoryProvider workflowHistoryProvider)
            : base(workflowProvider, pendingWorkflowItemProvider, workflowHistoryProvider)
        {
        }
        protected override bool IsAdministrator(string userName)
        {
            return ServiceFactory.UserManager.IsAdministrator(userName);
        }
        protected override string[] GetRoles(string userName)
        {
            if (Site.Current == null)
            {
                return new string[0];
            }
            var user = ServiceFactory.UserManager.Get(Models.Site.Current, userName);
            if (user == null || user.Roles == null)
            {
                return new string[0];
            }
            else
            {
                return user.Roles.ToArray();
            }
        }
    }
    public  class SendingSettingManager : Bsc.Dmtds.Content.Services.SendingSettingManager
    {
        public SendingSettingManager(ISendingSettingProvider provider)
            : base(provider)
        {
        }
        public override void Add(Repository repository, SendingSetting item)
        {
            base.Add(repository, item);
            AddReceivingSettingToSubSites(repository, item);
        }

        private void AddReceivingSettingToSubSites(Repository repository, SendingSetting item)
        {
            if (Site.Current != null && item.SendToChildSites.HasValue && item.SendToChildSites.Value == true)
            {
                var repositoryList = GetAllRepositoriesForChildSites(Site.Current, item.ChildLevel);

                foreach (var repo in repositoryList)
                {
                    try
                    {
                        if (repo != repository)
                        {
                            ReceivingSetting rece = new ReceivingSetting()
                            {
                                KeepStatus = item.KeepStatus,
                                ReceivingFolder = item.FolderName,
                                Repository = repo,
                                SendingFolder = item.FolderName,
                                SendingRepository = repository.Name
                            };

                            Bsc.Dmtds.Content.Services.ServiceFactory.ReceiveSettingManager.Add(repo, rece);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        public override void Update(Repository repository, SendingSetting @new, SendingSetting old)
        {
            base.Update(repository, @new, old);
            AddReceivingSettingToSubSites(repository, @new);
        }
        protected virtual IEnumerable<Repository> GetAllRepositoriesForChildSites(Site site, ChildLevel level)
        {
            List<Repository> repositoryList = new List<Repository>();

            GetAllRepositories(ServiceFactory.SiteManager.ChildSites(site), repositoryList, level);

            return repositoryList;
        }
        private void GetAllRepositories(IEnumerable<Site> sites, List<Repository> repositoryList, ChildLevel level)
        {
            foreach (var site in sites)
            {
                var repository = site.GetRepository();
                if (!repositoryList.Contains(repository))
                {
                    repositoryList.Add(repository);
                }
                if (level == ChildLevel.All)
                {
                    GetAllRepositories(ServiceFactory.SiteManager.ChildSites(site), repositoryList, level);
                }
            }
        }
    }

    public class ServiceFactory : IDependencyRegistrar
    {

        public static LayoutManager LayoutManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<LayoutManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<LayoutManager>(value);
            }
        }
        public static ViewManager ViewManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ViewManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ViewManager>(value);
            }
        }
        public static PageManager PageManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<PageManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<PageManager>(value);
            }
        }
        public static SiteManager SiteManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<SiteManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<SiteManager>(value);
            }
        }
        public static ScriptManager ScriptManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ScriptManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ScriptManager>(value);
            }
        }
        public static ThemeManager ThemeManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ThemeManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ThemeManager>(value);
            }
        }
        public static LabelManager LabelManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<LabelManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<LabelManager>(value);
            }
        }
        public static CustomErrorManager CustomErrorManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<CustomErrorManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<CustomErrorManager>(value);
            }
        }
        public static UrlRedirectManager UrlRedirectManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<UrlRedirectManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<UrlRedirectManager>(value);
            }
        }
        public static UrlKeyMapManager UrlKeyMapManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<UrlKeyMapManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<UrlKeyMapManager>(value);
            }
        }
        public static AssemblyManager AssemblyManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<AssemblyManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<AssemblyManager>(value);
            }
        }
        public static ModuleManager ModuleManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ModuleManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ModuleManager>(value);
            }
        }
        public static UserManager UserManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<UserManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<UserManager>(value);
            }
        }

        public static FileManager FileManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<FileManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<FileManager>(value);
            }
        }

        public static SiteTemplateManager SiteTemplateManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<SiteTemplateManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<SiteTemplateManager>(value);
            }
        }
        public static LayoutItemTemplateManager LayoutItemTemplateManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<LayoutItemTemplateManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<LayoutItemTemplateManager>(value);
            }
        }

        public static CodeSnippetManager CodeSnippetManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<CodeSnippetManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<CodeSnippetManager>(value);
            }
        }

        public static ImportedSiteManager ImportedSiteManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ImportedSiteManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ImportedSiteManager>(value);
            }
        }

        public static HtmlBlockManager HtmlBlockManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<HtmlBlockManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<HtmlBlockManager>(value);
            }
        }

        public static SystemManager SystemManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<SystemManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<SystemManager>(value);
            }
        }

        public static HeaderBackgroundManager HeaderBackgroundManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<HeaderBackgroundManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<HeaderBackgroundManager>(value);
            }
        }

        public static SubmissionSettingManager SubmissionSettingManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<SubmissionSettingManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<SubmissionSettingManager>(value);
            }
        }

        public static ABPageSettingManager ABPageSettingManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ABPageSettingManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ABPageSettingManager>(value);
            }
        }

        public static ABRuleSettingManager ABRuleSettingManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ABRuleSettingManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ABRuleSettingManager>(value);
            }
        }

        public static ABSiteSettingManager ABSiteSettingManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ABSiteSettingManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ABSiteSettingManager>(value);
            }
        }

        public static ABPageTestResultManager ABPageTestResultManager
        {
            get
            {
                return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ABPageTestResultManager>();
            }
            set
            {
                (Bsc.Dmtds.Core.Runtime.EngineContext.Current).ContainerManager.AddComponentInstance<ABPageTestResultManager>(value);
            }
        }
        public static T GetService<T>()
        where T : class
        {
            return Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<T>();
        }

        public void Register(IContainerManager containerManager, ITypeFinder typeFinder)
        {
            containerManager.AddComponent<Bsc.Dmtds.Content.Services.WorkflowManager, ContentWorkflowManager>();

            containerManager.AddComponent<Bsc.Dmtds.Content.Services.SendingSettingManager, SendingSettingManager>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}