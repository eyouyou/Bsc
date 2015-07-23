using System.Web.Mvc;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Extension
{
    public interface IHttpMethodPagePlugin : ICommonPagePlugin
    {        /// <summary>
        /// Write the page plug-in code when the page doing the Get request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="positionContext">The position context.</param>
        /// <returns></returns>
        ActionResult HttpGet(Page_Context context, PagePositionContext positionContext);

        /// <summary>
        /// Write the page plug-in code when the page doing the Post request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="positionContext">The position context.</param>
        /// <returns></returns>
        ActionResult HttpPost(Page_Context context, PagePositionContext positionContext);
         
    }
}