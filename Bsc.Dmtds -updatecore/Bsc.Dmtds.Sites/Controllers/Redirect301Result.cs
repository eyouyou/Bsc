using System.Web.Mvc;
using Bsc.Dmtds.Sites.Web;

namespace Bsc.Dmtds.Sites.Controllers
{
    public class Redirect301Result : RedirectResult
    {
        public Redirect301Result(string redirectUrl)
            : base(redirectUrl)
        {
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            var request = (FrontHttpRequestWrapper)httpContext.Request;

            context.HttpContext.Response.StatusCode = 301;
            context.HttpContext.Response.RedirectLocation = Url;
            context.HttpContext.Response.End();

        }
    }
}