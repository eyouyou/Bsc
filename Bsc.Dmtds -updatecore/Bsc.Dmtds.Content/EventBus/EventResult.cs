using System;

namespace Bsc.Dmtds.Content.EventBus
{
    public class EventResult
    {
        public Exception Exception { get; set; }

        public bool IsCancelled { get; set; } 
    }
}