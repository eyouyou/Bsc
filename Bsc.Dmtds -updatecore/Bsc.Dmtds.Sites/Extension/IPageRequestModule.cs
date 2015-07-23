using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Extension
{
    public interface IPageRequestModule
    {
        void OnResolvingSite(HttpContextBase httpContext);
        void OnResolvedSite(HttpContextBase httpContext);
        void OnResolvingPage(ControllerContext controllerContext);
        void OnResolvedPage(ControllerContext controllerContext, PageRequestContext pageRequestContext); 
    }
    public abstract class PageRequestModuleBase : IPageRequestModule
    {
        public virtual void OnResolvingSite(HttpContextBase httpContext) { }
        public virtual void OnResolvedSite(HttpContextBase httpContext) { }
        public virtual void OnResolvingPage(ControllerContext controllerContext) { }
        public virtual void OnResolvedPage(ControllerContext controllerContext, PageRequestContext pageRequestContext) { }
    }
}