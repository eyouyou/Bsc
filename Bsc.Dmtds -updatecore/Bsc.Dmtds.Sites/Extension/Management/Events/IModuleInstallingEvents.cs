using System.Web.Mvc;
using Bsc.Dmtds.Sites.Extension.ModuleArea;

namespace Bsc.Dmtds.Sites.Extension.Management.Events
{
    public interface IModuleInstallingEvents
    {
        /// <summary>
        /// Called when [installing].
        /// Will only enabled when the module have install template(The InstallingTemplate property in ModuleInfo)
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        /// <param name="controllerContext">The controller context.</param>
        void OnInstalling(ModuleContext moduleContext, ControllerContext controllerContext); 
    }
}