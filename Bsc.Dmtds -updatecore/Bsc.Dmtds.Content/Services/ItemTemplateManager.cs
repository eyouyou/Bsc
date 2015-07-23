using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Content.Services
{
    public class ItemTemplate
    {
        public string TemplateName { get; set; }
        public string Thumbnail { get; set; }
        public string TemplateFile { get; set; } 
    }
    /// <summary>
    /// 项目模板管理
    /// </summary>
    public abstract class ItemTemplateManager
    {
        protected abstract string TemplatePath { get; }
        protected virtual string FileExtension
        {
            get
            {
                return ".zip";
            }
        }
        public virtual IEnumerable<ItemTemplate> All()
        {
            var physicalPath = TemplatePath;
            if (Directory.Exists(physicalPath))
            {
                return Directory.EnumerateFiles(physicalPath, "*" + FileExtension).Select(it => GetItemTemplate(Path.GetFileNameWithoutExtension(it)));
            }
            return new ItemTemplate[0];
        }
        public virtual ItemTemplate GetItemTemplate(string templateName)
        {
            var physicalPath = TemplatePath;
            if (Directory.Exists(physicalPath))
            {
                ItemTemplate itemTemplate = new ItemTemplate() { TemplateName = templateName };
                itemTemplate.TemplateFile = Path.Combine(physicalPath, templateName + FileExtension);
                var thumbnail = Path.Combine(physicalPath, templateName + ".png");
                itemTemplate.Thumbnail = UrlUtility.GetVirtualPath(thumbnail);
                return itemTemplate;
            }
            return null;
        }
    }
}