namespace Bsc.Dmtds.Content.EventBus
{
    public interface ISubscriber
    {
        EventResult Receive(IEventContext context);
 
    }
}