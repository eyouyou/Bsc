
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;
using Ionic.Zip;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(LayoutItemTemplateManager))]
    public class LayoutItemTemplateManager : ItemTemplateManager
    {
        protected override string BasePath
        {
            get { return Bsc.Dmtds.Core.Mvc.AreaHelpers.CombineAreaFilePhysicalPath("Sites", "Templates", "Layout"); }
        }

        public virtual IEnumerable<LayoutSample> GetLayoutSamples(string engineName)
        {
            var itemTemplates = this.All(engineName);

            foreach (var item in itemTemplates)
            {
                using (ZipFile zipFile = new ZipFile(item.TemplateFile))
                {
                    var settingEntryName = PathResource.SettingFileName;
                    if (!zipFile.ContainsEntry(settingEntryName))
                    {
                        throw new BscException("The layout item template is invalid, setting.config is required.");
                    }
                    Layout layout = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        zipFile[settingEntryName].Extract(ms);
                        ms.Position = 0;
                        layout = (Layout)Bsc.Dmtds.Common.Util.DataContractSerializationHelper.Deserialize(typeof(Layout), null, ms);
                    }
                    var templateEntryName = layout.TemplateFileName;
                    if (!zipFile.ContainsEntry(templateEntryName))
                    {
                        throw new BscException(string.Format("The layout item template is invalid.{0} is requried.", templateEntryName));
                    }
                    LayoutSample sample = new LayoutSample() { Name = item.TemplateName, ThumbnailVirtualPath = item.Thumbnail };
                    using (MemoryStream ms = new MemoryStream())
                    {
                        zipFile[templateEntryName].Extract(ms);
                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms))
                        {
                            sample.Template = sr.ReadToEnd();
                        }
                    }
                    yield return sample;
                }
            }
        }

        public virtual LayoutSample GetDefaultLayoutSample(string engineName)
        {
            var defaultLayout = GetLayoutSample(engineName, "default");
            if (defaultLayout == null)
            {
                defaultLayout = GetLayoutSamples(engineName).FirstOrDefault();
            }
            return defaultLayout;
        }

        public virtual LayoutSample GetLayoutSample(string engineName, string templateName)
        {
            return GetLayoutSamples(engineName).Where(it => it.Name.EqualsOrNullEmpty(templateName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}