using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public class ModuleRequestContext : RequestContext
    {
        public ModuleRequestContext(HttpContextBase httpContext, RouteData routeData, ModuleContext moduleContext)
            : base(httpContext, routeData)
        {
            this.ModuleContext = moduleContext;

        }
        public ModuleContext ModuleContext { get; private set; }
        public ControllerContext PageControllerContext { get; set; }

    }
}