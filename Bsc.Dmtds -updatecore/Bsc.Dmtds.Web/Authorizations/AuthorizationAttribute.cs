using System;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;
using Bsc.Dmtds.Service;

namespace Bsc.Dmtds.Web.Authorizations
{
    public class AuthorizationAttribute
    {
         
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RequiredLogOnAttribute : FilterAttribute, IAuthorizationFilter
    {
        public readonly IUserService UserService;

        public RequiredLogOnAttribute(IUserService userService)
        {
            UserService = userService;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var requestContext = filterContext.RequestContext;
            var exclusiveFlag = "AuthorizationExclusive";
            var exclusive = requestContext.HttpContext.Items[exclusiveFlag] == null ? false : (bool)requestContext.HttpContext.Items[exclusiveFlag];
            if (!exclusive)
            {
                if (this.AuthorizeCore(filterContext.RequestContext))
                {
                    //HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                    //cache.SetProxyMaxAge(new TimeSpan(0L));
                    //cache.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler), null);
                }
                else
                {
                    this.HandleUnauthorizedRequest(filterContext);
                }
            }
            requestContext.HttpContext.Items[exclusiveFlag] = Exclusive;
        }
        //private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        //{
        //    validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
        //}
        //protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        //{
        //    if (!this.AuthorizeCore(httpContext))
        //    {
        //        return HttpValidationStatus.IgnoreThisRequest;
        //    }
        //    return HttpValidationStatus.Valid;
        //}
        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        protected virtual bool AuthorizeCore(RequestContext requestContext)
        {
            var userName = requestContext.HttpContext.User.Identity.Name;
            var authenticated = requestContext.HttpContext.User.Identity.IsAuthenticated;
            if (authenticated && RequiredAdministrator)
            {
                return UserService.IsAdministrator(userName);
            }
//            else
//            {
//                if (Site.Current != null)
//                {
//                    authenticated = Kooboo.CMS.Sites.Services.ServiceFactory.UserManager.Authorize(Site.Current, userName);
//                }
//
//                return authenticated;
//            }
            return false;
        }
        public bool RequiredAdministrator { get; set; }
        public bool Exclusive { get; set; }
    }

}