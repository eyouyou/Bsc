﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Extension.ModuleArea;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Parsers.ThemeRule;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(ModuleManager))]
    public class ModuleManager
    {
        #region Module Script & theme
        private ModuleItemPath GetThemesBasePath(string moduleName)
        {
            return new ModuleItemPath(moduleName, "Themes");
        }
        private ModuleItemPath GetThemePath(string moduleName, string themeName)
        {
            return new ModuleItemPath(GetThemesBasePath(moduleName), themeName);
        }
        public virtual IEnumerable<string> AllThemes(string moduleName)
        {
            var themesPath = GetThemesBasePath(moduleName).PhysicalPath;
            if (Directory.Exists(themesPath))
            {
                foreach (var dir in Directory.EnumerateDirectories(themesPath))
                {
                    yield return Path.GetFileName(dir);
                }
            }
        }
        public virtual IEnumerable<ModuleItemPath> AllScripts(string moduleName)
        {
            ModuleItemPath scriptsPath = new ModuleItemPath(moduleName, "Scripts");
            if (Directory.Exists(scriptsPath.PhysicalPath))
            {
                var files = Directory.EnumerateFiles(scriptsPath.PhysicalPath, "*.js").Select(it => Path.GetFileName(it));
                var orderFilePath = Path.Combine(scriptsPath.PhysicalPath, FileOrderHelper.OrderFileName);
                if (File.Exists(orderFilePath))
                {
                    files = FileOrderHelper.OrderFiles(orderFilePath, files);
                }
                foreach (var file in files)
                {
                    yield return new ModuleItemPath(scriptsPath, file);
                }
            }
        }
        public virtual IEnumerable<ModuleItemPath> AllThemeFiles(string moduleName, string themeName, out string themeRuleBody)
        {
            ModuleItemPath themePath = GetThemePath(moduleName, themeName);
            List<ModuleItemPath> themeFiles = new List<ModuleItemPath>();
            themeRuleBody = "";
            if (Directory.Exists(themePath.PhysicalPath))
            {
                var files = Directory.EnumerateFiles(themePath.PhysicalPath, "*.css").Select(it => Path.GetFileName(it));
                var orderFilePath = Path.Combine(themePath.PhysicalPath, FileOrderHelper.OrderFileName);
                if (File.Exists(orderFilePath))
                {
                    files = FileOrderHelper.OrderFiles(orderFilePath, files);
                }

                foreach (var file in files)
                {
                    themeFiles.Add(new ModuleItemPath(themePath, file));
                }

                string themeBaseUrl = UrlUtility.ResolveUrl(themePath.VirtualPath);

                var themeRuleFiles = ThemeRuleParser.Parser.Parse(ThemeRuleBody(moduleName, themeName),
                    (fileVirtualPath) => UrlUtility.Combine(themeBaseUrl, fileVirtualPath), out themeRuleBody);

                return themeFiles.Where(it => !themeRuleFiles.Any(cf => cf.EqualsOrNullEmpty(it.EntryName, StringComparison.CurrentCultureIgnoreCase)));
            }
            return new ModuleItemPath[0];

        }
        private string ThemeRuleBody(string moduleName, string themeName)
        {
            ModuleItemPath themePath = GetThemePath(moduleName, themeName);
            ModuleItemPath themeRuleFile = new ModuleItemPath(themePath, "Theme.rule");
            if (File.Exists(themeRuleFile.PhysicalPath))
            {
                return File.ReadAllText(themeRuleFile.PhysicalPath);
            }
            return string.Empty;
        }
        #endregion

        #region Management

        #region All
        public virtual IEnumerable<string> All()
        {
            var baseDirectory = ModulePath.BaseDirectory;
            if (Directory.Exists(baseDirectory))
            {
                foreach (var dir in IOUtility.EnumerateDirectoriesExludeHidden(baseDirectory))
                {
                    var moduleName = dir.Name;

                    var moduleConfigFile = new ModuleItemPath(moduleName, ModuleInfo.ModuleInfoFileName);

                    if (File.Exists(moduleConfigFile.PhysicalPath))
                    {
                        yield return moduleName;
                    }
                }
            }
        }
        #endregion

        #region AllModuleInfo
        public virtual IEnumerable<ModuleInfo> AllModuleInfo()
        {
            foreach (var name in All())
            {
                yield return Get(name);
            }
        }
        #endregion

        #region Get
        public virtual ModuleInfo Get(string moduleName)
        {
            return ModuleInfo.Get(moduleName);
        }
        #endregion
        #endregion

        #region ResolveModuleAction
        protected virtual Bsc.Dmtds.Sites.Extension.Management.Events.IModuleSiteRelationEvents ResolveModuleAction(string moduleName)
        {
            var moduleEvent = Bsc.Dmtds.Core.Runtime.EngineContext.Current.TryResolve<Bsc.Dmtds.Sites.Extension.Management.Events.IModuleSiteRelationEvents>(moduleName);
            if (moduleEvent == null)
            {
                moduleEvent = Bsc.Dmtds.Core.Runtime.EngineContext.Current.TryResolve<IModuleEvents>(moduleName);
            }
            return moduleEvent;
        }
        #endregion

        #region Site&Module relation
        private static class ModuleRelationData
        {
            static System.Threading.ReaderWriterLockSlim sitesModuleRelationLocker = new System.Threading.ReaderWriterLockSlim();
            public static List<string> GetSitesInModule(string moduleName)
            {
                var filePath = GetSitesModuleRelationDataFile(moduleName);
                if (!File.Exists(filePath))
                {
                    return new List<string>();
                }
                sitesModuleRelationLocker.EnterReadLock();
                try
                {
                    var jsonData = IOUtility.ReadAsString(filePath);
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(jsonData);
                    return list;
                }
                finally
                {
                    sitesModuleRelationLocker.ExitReadLock();
                }
            }
            public static void SaveSitesInModule(string moduleName, List<string> sites)
            {
                var filePath = GetSitesModuleRelationDataFile(moduleName);
                sitesModuleRelationLocker.EnterWriteLock();
                try
                {
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(sites, Newtonsoft.Json.Formatting.Indented);
                    IOUtility.SaveStringToFile(filePath, jsonData);
                }
                finally
                {
                    sitesModuleRelationLocker.ExitWriteLock();
                }
            }
            private static string GetSitesModuleRelationDataFile(string moduleName)
            {
                var modulePath = new ModulePath(moduleName);
                var relation = modulePath.GetModuleSharedFilePath("Sites.txt");
                return relation.PhysicalPath;
            }
        }
        public virtual void AddSiteToModule(string moduleName, string siteName)
        {
            var list = ModuleRelationData.GetSitesInModule(moduleName);
            if (!list.Contains(siteName, StringComparer.OrdinalIgnoreCase))
            {
                list.Add(siteName);
                ModuleRelationData.SaveSitesInModule(moduleName, list);
            }
            try
            {
                var moduleAction = ResolveModuleAction(moduleName);
                if (moduleAction != null)
                {
                    moduleAction.OnIncluded(ModuleContext.Create(moduleName, new Site(siteName)));
                }
            }
            catch (Exception e)
            {
                Bsc.Dmtds.Common.HealthMonitoring.Log.LogException(e);
            }
        }

        public virtual void RemoveSiteFromModule(string moduleName, string siteName)
        {
            var list = ModuleRelationData.GetSitesInModule(moduleName);
            list.RemoveAll(s => s.EqualsOrNullEmpty(siteName, StringComparison.OrdinalIgnoreCase));
            ModuleRelationData.SaveSitesInModule(moduleName, list);

            try
            {
                var moduleAction = ResolveModuleAction(moduleName);
                if (moduleAction != null)
                {
                    moduleAction.OnExcluded(ModuleContext.Create(moduleName, new Site(siteName)));
                }
            }
            catch (Exception e)
            {
                Bsc.Dmtds.Common.HealthMonitoring.Log.LogException(e);
            }

        }
        public virtual IEnumerable<Site> AllSitesInModule(string moduleName)
        {
            return ModuleRelationData.GetSitesInModule(moduleName).Select(it => new Site(it).AsActual()).Where(it => it != null);
        }
        public virtual bool SiteIsInModule(string moduleName, string siteName)
        {
            return ModuleRelationData.GetSitesInModule(moduleName).Contains(siteName, StringComparer.OrdinalIgnoreCase);
        }
        public virtual IEnumerable<string> AllModulesForSite(string siteName)
        {
            foreach (var moduleName in All())
            {
                if (SiteIsInModule(moduleName, siteName))
                {
                    yield return moduleName;
                }
            }
        }
        #endregion
    }
}