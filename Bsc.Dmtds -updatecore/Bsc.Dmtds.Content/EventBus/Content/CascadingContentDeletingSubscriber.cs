using System;
using System.Threading.Tasks;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Query;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class CascadingContentDeletingSubscriber : ISubscriber
    {
        public EventResult Receive(IEventContext context)
        {
            EventResult eventResult = new EventResult();

            if (context is ContentEventContext)
            {
                var contentEventContext = (ContentEventContext)context;
                if (contentEventContext.ContentAction == Models.ContentAction.Delete)
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            var textFolder = contentEventContext.Content.GetFolder().AsActual();

                            // Delete the child contents in this folder.
                            DeleteChildContents(contentEventContext.Content, textFolder, textFolder);

                            if (textFolder.EmbeddedFolders != null)
                            {
                                foreach (var folderName in textFolder.EmbeddedFolders)
                                {
                                    var childFolder = new TextFolder(textFolder.Repository, folderName);
                                    DeleteChildContents(contentEventContext.Content, textFolder, childFolder);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            eventResult.Exception = e;
                        }

                    });
                }
            }

            return eventResult;
        }

        private static void DeleteChildContents(TextContent textContent, TextFolder parentFolder, TextFolder childFolder)
        {
            var repository = textContent.GetRepository();
            var childContents = childFolder.CreateQuery().WhereEquals("ParentFolder", parentFolder.FullName)
                .WhereEquals("ParentUUID", textContent.UUID);
            foreach (var content in childContents)
            {
                Services.ServiceFactory.TextContentManager.Delete(repository, childFolder, content.UUID);
            }
        }
    }
}