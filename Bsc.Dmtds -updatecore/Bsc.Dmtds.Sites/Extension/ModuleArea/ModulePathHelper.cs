using System;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public static class ModulePathHelper
    {

        const string ModuleDataFolderName = "ModuleData";

        #region GetModuleInstallationFilePath
        /// <summary>
        /// GetModuleInstallationFilePath 是得到Module的安装目录，也就是它所在的Area目录
        /// </summary>
        /// <param name="modulePath">The module path.</param>
        /// <param name="moduleInstallationFilePaths">The module installation file paths.</param>
        /// <returns></returns>
        public static IPath GetModuleInstallationFilePath(this ModulePath modulePath, params string[] moduleInstallationFilePaths)
        {
            if (moduleInstallationFilePaths != null && moduleInstallationFilePaths.Length > 0)
            {
                return new CommonPath()
                {
                    PhysicalPath = Path.Combine(new[] { modulePath.PhysicalPath }.Concat(moduleInstallationFilePaths).ToArray()),
                    VirtualPath = UrlUtility.Combine(new[] { modulePath.VirtualPath }.Concat(moduleInstallationFilePaths).ToArray())
                };
            }
            return modulePath;
        }
        #endregion

        #region GetModuleSharedFilePath
        /// <summary>
        /// GetModuleSharedFilePath 是得到它共享的数据存放目录，与站点无关的，也就是：Cms_Data\ModuleData
        /// </summary>
        /// <param name="modulePath"></param>
        /// <param name="extraPaths"></param>
        /// <returns></returns>
        public static IPath GetModuleSharedFilePath(this ModulePath modulePath, params string[] extraPaths)
        {
            var baseDir = Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<IBaseDir>();
            var physicalPaths = new[] { baseDir.DataPhysicalPath, ModuleDataFolderName, modulePath.ModuleName };
            var virtualPaths = new[] { baseDir.DataVirtualPath, ModuleDataFolderName, modulePath.ModuleName };
            if (extraPaths != null && extraPaths.Length > 0)
            {
                physicalPaths = physicalPaths.Concat(extraPaths).ToArray();
                virtualPaths = virtualPaths.Concat(extraPaths).ToArray();
            }
            var path = new CommonPath()
            {
                PhysicalPath = Path.Combine(physicalPaths),
                VirtualPath = UrlUtility.Combine(virtualPaths)
            };

            return path;

        }
        #endregion

        #region GetModuleLocalFilePath
        /// <summary>
        /// GetModuleLocalFilePath是得到Module在某个站点下的ModuleData，也就是：Bsc_Data\Sites\{SiteName}\ModuleData
        /// 需要ModulePath.Site信息。
        /// </summary>
        /// <param name="modulePath"></param>
        /// <param name="extraPaths"></param>
        /// <returns></returns>
        public static IPath GetModuleLocalFilePath(this ModulePath modulePath, params string[] extraPaths)
        {
            if (modulePath.Site == null)
            {
                throw new Exception("该站点为空, can not get the local file path");
            }
            var physicalPaths = new[] { modulePath.Site.PhysicalPath, ModuleDataFolderName, modulePath.ModuleName };
            var virtualPaths = new[] { modulePath.Site.VirtualPath, ModuleDataFolderName, modulePath.ModuleName };
            if (extraPaths != null && extraPaths.Length > 0)
            {
                physicalPaths = physicalPaths.Concat(extraPaths).ToArray();
                virtualPaths = virtualPaths.Concat(extraPaths).ToArray();
            }
            var path = new CommonPath()
            {
                PhysicalPath = Path.Combine(physicalPaths),
                VirtualPath = UrlUtility.Combine(virtualPaths)
            };

            return path;
        }
        #endregion 
    }
}