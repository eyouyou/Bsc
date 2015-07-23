using System.Web.Mvc;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Web;

namespace Bsc.Dmtds.Sites.View
{
    public static class UrlExtensions
    {
        public static FrontUrlHelper FrontUrl(this UrlHelper url)
        {
            if (Page_Context.Current.Initialized)
            {
                return Page_Context.Current.FrontUrl;
            }
            //Throw "Unable to cast object of type 'Kooboo.CMS.Sites.Extension.ModuleArea.ModuleHttpRequest' to type 'Kooboo.CMS.Sites.Web.FrontHttpRequestWrapper'."
            FrontRequestChannel requestChannel = FrontRequestChannel.Unknown;
            if (url.RequestContext.HttpContext.Request is FrontHttpRequestWrapper)
            {
                requestChannel = ((FrontHttpRequestWrapper)url.RequestContext.HttpContext.Request).RequestChannel;
            }
            return new FrontUrlHelper(url, Site.Current, requestChannel);
        }
        public static FrontUrlHelper FrontUrl(this UrlHelper url, Site site, FrontRequestChannel requestChannel = FrontRequestChannel.Host)
        {
            return new FrontUrlHelper(url, site, requestChannel);
        } 
    }
}