using System;
using System.Web.Mvc;

namespace Bsc.Dmtds.Sites
{
    [ValidateInput(false)]    
    public class AreaControllerBase : Controller
    {
        #region Initialize
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }
        #endregion

        #region  OnAuthorization
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var forceSSL = false;

            var forceSSLSetting = System.Configuration.ConfigurationManager.AppSettings["forceSSL_AdminPages"];
            if (!string.IsNullOrEmpty(forceSSLSetting))
            {
                forceSSL = forceSSLSetting.ToLower() == "true";
            }

            if (forceSSL && !filterContext.HttpContext.Request.IsSecureConnection)
            {
                this.HandleNonHttpsRequest(filterContext);
            }

        }
        protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("MvcResources.RequireHttpsAttribute_MustUseSsl");
            }
            string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url);
        }


        #endregion
    }
}