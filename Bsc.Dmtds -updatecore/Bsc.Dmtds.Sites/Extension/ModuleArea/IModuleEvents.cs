using Bsc.Dmtds.Sites.Extension.Management.Events;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public interface IModuleEvents : IModuleInstallingEvents, IModuleUninstallingEvents, IModuleSiteRelationEvents, IModuleReinstallingEvents
    {
         
    }
}