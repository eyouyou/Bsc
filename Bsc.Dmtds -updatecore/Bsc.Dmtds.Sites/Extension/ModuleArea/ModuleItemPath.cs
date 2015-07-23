using System.IO;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public class ModuleItemPath : IPath
    {
        #region .ctor
        public ModuleItemPath(string moduleName, string entryName)
        {
            ModulePath modulePath = new ModulePath(moduleName);

            EntryName = entryName;
            PhysicalPath = Path.Combine(modulePath.PhysicalPath, EntryName);
            VirtualPath = UrlUtility.Combine(modulePath.VirtualPath, EntryName);
        }
        public ModuleItemPath(ModuleItemPath parent, string entryName)
        {
            EntryName = entryName;
            PhysicalPath = Path.Combine(parent.PhysicalPath, EntryName);
            VirtualPath = UrlUtility.Combine(parent.VirtualPath, EntryName);
        }
        #endregion

        #region Properties
        public string EntryName { get; private set; }
        public string PhysicalPath { get; private set; }
        public string VirtualPath { get; private set; }
        #endregion
    }
}