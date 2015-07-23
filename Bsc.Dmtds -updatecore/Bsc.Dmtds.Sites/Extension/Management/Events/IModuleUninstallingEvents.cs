using System.Web.Mvc;
using Bsc.Dmtds.Sites.Extension.ModuleArea;

namespace Bsc.Dmtds.Sites.Extension.Management.Events
{
    public interface IModuleUninstallingEvents
    {
        /// <summary>
        /// Called when [uninstalling].
        /// Will only enabled when the module have install template(The InstallingTemplate property in ModuleInfo)
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        /// <param name="controllerContext">The controller context.</param>
        void OnUninstalling(ModuleContext moduleContext, ControllerContext controllerContext); 
    }
}