using System;
using System.Collections.Generic;
using Bsc.Dmtds.Core.Mvc.WebResourceLoader.Configuration;

namespace Bsc.Dmtds.Core.Mvc.WebResourceLoader
{
    public static class ConfigurationManager
    {
        static WebResourcesSection defaultSection;
        static IDictionary<string, WebResourcesSection> areasSection = new Dictionary<string, WebResourcesSection>(StringComparer.CurrentCultureIgnoreCase);

        static ConfigurationManager()
        {
            defaultSection = WebResourcesSection.GetSection();
        }
        public static void RegisterSection(string area, string configFile)
        {
            lock (areasSection)
            {
                WebResourcesSection section = WebResourcesSection.GetSection(configFile);
                areasSection.Add(area, section);
            }
        }
        public static WebResourcesSection GetSection(string area)
        {
            WebResourcesSection section = null;

            if (!string.IsNullOrEmpty(area) && areasSection.ContainsKey(area))
            {
                section = areasSection[area];
            }
            if (section == null)
            {
                section = defaultSection;
            }
            if (section == null)
            {
                throw new WebResourceException("找不到网络资源配置节点");
            }
            return section;
        }
    } 
    
}