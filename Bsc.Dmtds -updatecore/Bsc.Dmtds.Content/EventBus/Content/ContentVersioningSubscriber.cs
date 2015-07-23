using System;
using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class ContentVersioningSubscriber : ISubscriber
    {
        #region ISubscriber Members

        public EventResult Receive(IEventContext context)
        {
            EventResult eventResult = new EventResult();
            if (context is ContentEventContext)
            {
                try
                {
                    var contentContext = ((ContentEventContext)context);
                    if (contentContext.Content.___EnableVersion___)
                    {
                        if (contentContext.Content is TextContent)
                        {
                            switch (contentContext.ContentAction)
                            {
                                case Models.ContentAction.Delete:
                                    break;
                                case Models.ContentAction.Add:
                                case Models.ContentAction.Update:
                                    Versioning.VersionManager.LogVersion((TextContent)(contentContext.Content));
                                    break;
                                default:

                                    break;
                            }
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

        #endregion
    }
}