using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public static class ContentEvent
    {
        static ContentEvent()
        {
            EventBus.Subscribers.Add(new ContentSequenceSubscriber());
            EventBus.Subscribers.Add(new ContentBroadcastingSubscriber());
            EventBus.Subscribers.Add(new ContentVersioningSubscriber());
            EventBus.Subscribers.Add(new ContentWorkflowSubscriber());
            EventBus.Subscribers.Add(new ContentImageCropSubscriber());
            EventBus.Subscribers.Add(new CascadingContentDeletingSubscriber());
        }

        public static void Fire(ContentAction contentAction, TextContent textContent)
        {
            EventBus.Send(new ContentEventContext(contentAction, textContent));
        } 
    }
}