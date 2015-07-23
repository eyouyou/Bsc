using System;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class ContentWorkflowSubscriber : ISubscriber
    {
        public EventResult Receive(IEventContext context)
        {
            EventResult eventResult = new EventResult();
            if (context is ContentEventContext)
            {
                try
                {
                    ContentEventContext contentEventContext = (ContentEventContext)context;
                    if (contentEventContext.ContentAction == ContentAction.Add && contentEventContext.Content is TextContent)
                    {
                        var textContent = (TextContent)contentEventContext.Content;
                        var folder = (TextFolder)textContent.GetFolder().AsActual();
                        if (folder.EnabledWorkflow && !string.IsNullOrEmpty(contentEventContext.Content.UserId))
                        {
                            Services.ServiceFactory.WorkflowManager.StartWorkflow(textContent.GetRepository()
                                , folder.WorkflowName
                                , textContent
                                , contentEventContext.Content.UserId);
                        }
                    }
                }
                catch (Exception e)
                {
                    eventResult.Exception = e;
                }

            }
            return eventResult;
        }
    }
}