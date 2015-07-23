using System.Web.Mvc;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Extension
{
    public interface IPagePlugin : ICommonPagePlugin
    {
        /// <summary>
        /// Executes the specified page view context.
        /// </summary>
        /// <param name="pageContext">The page context.</param>
        /// <param name="positionContext">The value will be null when executing a plugin in page.</param>
        /// <returns></returns>
        ActionResult Execute(Page_Context pageContext, PagePositionContext positionContext);
    }
}