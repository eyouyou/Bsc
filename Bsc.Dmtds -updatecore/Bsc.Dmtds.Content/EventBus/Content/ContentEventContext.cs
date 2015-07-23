using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class ContentEventContext : IEventContext
    {
        public ContentEventContext(ContentAction contentAction, TextContent content)
        {
            this.ContentAction = contentAction;
            this.Content = content;
        }
        public ContentAction ContentAction { get; set; }

        #region IEventContext Members

        public object State
        {
            get
            {
                return this.Content;
            }
        }
        #endregion

        public TextContent Content { get; private set; }
    }
}