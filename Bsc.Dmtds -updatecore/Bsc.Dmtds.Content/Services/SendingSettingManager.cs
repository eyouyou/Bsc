using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Persistence;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Services
{
    public class RepositorySendingFolders
    {
        public Repository Repository { get; set; }
        public IEnumerable<FolderTreeNode<TextFolder>> TextFolders { get; set; }
    }
    [Dependency(typeof(SendingSettingManager))]
    public class SendingSettingManager : ManagerBase<SendingSetting, ISendingSettingProvider>
    {
        public SendingSettingManager(ISendingSettingProvider provider)
            : base(provider)
        {
        }
        public override SendingSetting Get(Repository repository, string name)
        {
            return Provider.Get(new SendingSetting { Repository = repository, Name = name });
        }
        public virtual IEnumerable<RepositorySendingFolders> GetAllSendingFolders()
        {
            List<RepositorySendingFolders> repositorySendingFolders = new List<RepositorySendingFolders>();

            IEnumerable<Repository> repositories = ServiceFactory.RepositoryManager.All();
            foreach (var repository in repositories)
            {
                var treeNodes = GetTreeNodes(repository, Services.ServiceFactory.SendingSettingManager.All(repository, ""));
                if (treeNodes.Count() > 0)
                {
                    repositorySendingFolders.Add(new RepositorySendingFolders()
                    {
                        Repository = repository,
                        TextFolders = treeNodes
                    });
                }
            }

            return repositorySendingFolders;
        }
        protected IEnumerable<FolderTreeNode<TextFolder>> GetTreeNodes(Repository repository, IEnumerable<SendingSetting> sendingSettings)
        {
            List<FolderTreeNode<TextFolder>> list = new List<FolderTreeNode<TextFolder>>();

            var allFolders = sendingSettings.Select(it => it.FolderName).OrderBy(it => FolderHelper.SplitFullName(it).Count());

            foreach (var folderName in allFolders)
            {
                list.Add(new FolderTreeNode<TextFolder>()
                {
                    Folder = new TextFolder(repository, folderName)
                });
            }
            return list;
        }
    }
}