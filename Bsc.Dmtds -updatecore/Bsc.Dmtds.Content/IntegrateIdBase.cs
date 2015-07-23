using System;

namespace Bsc.Dmtds.Content
{
    public class IntegrateIdBase
    {
        public string[] Segments { get; protected set; }
        public IntegrateIdBase() { }
        public IntegrateIdBase(string id)
        {
            Splite(id);
        }
        private void Splite(string contentId)
        {
            if (!string.IsNullOrEmpty(contentId))
            {
                Segments = contentId.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public string Id
        {
            get
            {
                if (Segments != null)
                {
                    return string.Join("#", Segments);
                }
                return null;
            }
        }
        public override string ToString()
        {
            return Id;
        } 
    }
}