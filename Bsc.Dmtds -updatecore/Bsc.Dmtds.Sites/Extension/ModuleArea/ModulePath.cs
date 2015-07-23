using System;
using System.IO;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public class ModulePath : IPath
    {
        #region Accessors
        public static Func<string, string> PhysicalPathAccessor = moduleName => Path.Combine(BaseDirectory, moduleName);
        public static Func<string, string> VirtualPathAccessor = moduleName => UrlUtility.Combine(BaseVirutalPath, moduleName);
        #endregion

        #region .ctor
        static ModulePath()
        {
            BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Areas");
            BaseVirutalPath = UrlUtility.Combine("~/", "Areas");
        }
        public ModulePath(string moduleName)
            : this(moduleName, null)
        { }
        public ModulePath(string moduleName, Site site)
        {
            ModuleName = moduleName;
            Site = site;
            PhysicalPath = PhysicalPathAccessor(moduleName);
            VirtualPath = VirtualPathAccessor(moduleName);
        }
        #endregion

        public string ModuleName { get; private set; }
        public Site Site { get; set; }

        #region Module installation base path
        public static string BaseDirectory { get; private set; }
        public static string BaseVirutalPath { get; private set; }

        #endregion

        #region Installation path

        public string PhysicalPath { get; private set; }
        public string VirtualPath { get; private set; }
        #endregion
    }
}