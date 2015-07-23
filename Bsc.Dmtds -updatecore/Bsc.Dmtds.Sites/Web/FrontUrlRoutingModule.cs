using System;
using System.Web;
using System.Web.Routing;

namespace Bsc.Dmtds.Sites.Web
{
    public class FrontUrlRoutingModule : UrlRoutingModule
    {
        protected override void Init(HttpApplication application)
        {
            application.PostResolveRequestCache += new EventHandler(application_PostResolveRequestCache);
        }

        void application_PostResolveRequestCache(object sender, EventArgs e)
        {
            HttpContextBase context = new FrontHttpContextWrapper(((HttpApplication)sender).Context);
            this.PostResolveRequestCache(context);
        }

    }
}