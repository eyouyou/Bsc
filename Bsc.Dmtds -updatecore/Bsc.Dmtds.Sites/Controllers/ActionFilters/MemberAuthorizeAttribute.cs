using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Sites.Membership;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Controllers.ActionFilters
{
    public class MemberAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }
        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!Page_Context.Current.Initialized)
            {
                throw new InvalidOperationException();
            }
            var permission = Page_Context.Current.PageRequestContext.Page.Permission;
            if (permission != null)
            {
                IPrincipal member = httpContext.Membership().GetMember();
                return permission.Authorize(member);
            }

            return true;
        }

        protected virtual void HandleUnauthorizedRequest(ActionExecutingContext filterContext)
        {
            var permission = Page_Context.Current.PageRequestContext.Page.Permission;
            if (permission != null && !string.IsNullOrEmpty(permission.UnauthorizedUrl))
            {
                var unauthorizedUrl = permission.UnauthorizedUrl.AddQueryParam("returnUrl", filterContext.HttpContext.Request.RawUrl);
                var redirectUrl = FrontUrlHelper.WrapperUrl(unauthorizedUrl.TrimStart('~'), Page_Context.Current.PageRequestContext.Site, Page_Context.Current.PageRequestContext.RequestChannel);
                filterContext.Result = new RedirectResult(redirectUrl.ToString());
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.Unauthorized, "The page available for member only.");
            }

        }
    }
}