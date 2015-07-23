using System.Web.Mvc;
using Bsc.Dmtds.Sites.Extension.ModuleArea;

namespace Bsc.Dmtds.Sites.Extension.Management.Events
{
    public interface IModuleReinstallingEvents
    {
        void OnReinstalling(ModuleContext moduleContext, ControllerContext controllerContext, InstallationContext installationContext);
 
    }
}