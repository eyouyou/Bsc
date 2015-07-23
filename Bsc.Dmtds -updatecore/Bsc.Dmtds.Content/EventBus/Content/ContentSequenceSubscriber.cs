using System;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Query;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class ContentSequenceSubscriber : ISubscriber
    {
        public EventResult Receive(IEventContext context)
        {
            EventResult eventResult = new EventResult();
            if (context is ContentEventContext)
            {
                try
                {
                    var contentEventContext = (ContentEventContext)context;
                    switch (contentEventContext.ContentAction)
                    {
                        case Models.ContentAction.Add:
                            break;
                        case Models.ContentAction.Update:
                            break;
                        case Models.ContentAction.Delete:
                            break;
                        case Models.ContentAction.PreAdd:
                            if (contentEventContext.Content.Sequence == 0)
                            {
                                var textFolder = contentEventContext.Content.GetFolder().AsActual();
                                int sequence = 1;
                                var maxSequenceContent = textFolder.CreateQuery().OrderByDescending("Sequence").FirstOrDefault();
                                if (maxSequenceContent != null)
                                {
                                    sequence = maxSequenceContent.Sequence + 1;
                                }
                                contentEventContext.Content.Sequence = sequence;

                            }

                            break;
                        case Models.ContentAction.PreUpdate:
                            break;
                        case Models.ContentAction.PreDelete:
                            break;
                        default:
                            break;
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