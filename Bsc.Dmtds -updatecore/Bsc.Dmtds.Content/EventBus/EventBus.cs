using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Content.EventBus
{
    public class EventBus
    {
        static EventBus()
        {
            Subscribers = new List<ISubscriber>();
        }
        public static List<ISubscriber> Subscribers { get; set; }

        public static void Send(IEventContext eventContext)
        {
            foreach (var s in ResolveAllSubscribers())
            {
                var eventResult = s.Receive(eventContext);
                if (eventResult != null)
                {
                    if (eventResult.Exception != null)
                    {
                        Bsc.Dmtds.Common.HealthMonitoring.Log.LogException(eventResult.Exception);
                    }
                    if (eventResult.IsCancelled == true)
                    {
                        break;
                    }
                }
            }
        }
        private static IEnumerable<ISubscriber> ResolveAllSubscribers()
        {
            return Subscribers.Concat(EngineContext.Current.ResolveAll<ISubscriber>());
        } 
    }
}