using System;
using System.Web.Mvc;

namespace Bsc.Dmtds.Web.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.HttpContext.Response.Redirect("../Home/Index");
            //base.OnActionExecuting(filterContext);
        }
    }
}