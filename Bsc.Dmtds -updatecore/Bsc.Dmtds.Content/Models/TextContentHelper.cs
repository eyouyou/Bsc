using System;
using System.Linq;
using Bsc.Dmtds.Core;

namespace Bsc.Dmtds.Content.Models
{
    public static class TextContentHelper
    {
        public static TextContent ConvertToUTCTime(this TextContent textContent)
        {
            ITimeZoneHelper timeZoneHelper = Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ITimeZoneHelper>();
            TextContent newTextContent = new TextContent(textContent);

            foreach (var key in newTextContent.Keys.ToArray())
            {
                if (newTextContent[key] is DateTime)
                {
                    DateTime dt = (DateTime)newTextContent[key];
                    if (dt.Kind != DateTimeKind.Utc)
                    {
                        newTextContent[key] = timeZoneHelper.ConvertToUtcTime(dt);
                    }
                }
            }

            return newTextContent;
        }

        public static TextContent ConvertToLocalTime(this TextContent textContent)
        {
            ITimeZoneHelper timeZoneHelper = Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<ITimeZoneHelper>();
            TextContent newTextContent = new TextContent(textContent);

            foreach (var key in newTextContent.Keys.ToArray())
            {
                if (newTextContent[key] is DateTime)
                {
                    DateTime dt = (DateTime)newTextContent[key];
                    if (dt.Kind != DateTimeKind.Local)
                    {
                        newTextContent[key] = timeZoneHelper.ConvertToLocalTime(dt, TimeZoneInfo.Utc);
                    }
                }
            }

            return newTextContent;
        }
    }
}