using System.Collections.Generic;
using System.Web;

namespace Bsc.Dmtds.Sites.View.CodeSnippet
{
    public interface ILayoutPositionParser
    {
        IEnumerable<string> Parse(string layoutBody);

        IHtmlString RegisterClientParser();
        IHtmlString RegisterClientAddPosition();
        IHtmlString RegisterClientRemovePosition();
    }
}