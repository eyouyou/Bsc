using System.IO;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content.Models.Paths;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.Models
{
    public static class FolderExtensions
    {
        /// <summary>
        /// 取目录对应的Schema(Content type)
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        public static Schema GetSchema(this TextFolder folder)
        {
            var schemaName = folder.AsActual().SchemaName;
            if (string.IsNullOrEmpty(schemaName))
            {
                throw new BscException(string.Format("文件夹 '{0}' 不是一个内容文件夹.", folder.FriendlyName));
            }
            return new Schema(folder.Repository, schemaName);
        }


        /// <summary>
        /// 查找内容目录下是否有自定义的内容模板，如果有，则优先使用该模板。
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="formType">Type of the form.</param>
        /// <returns></returns>
        public static string GetFormTemplate(this TextFolder folder, FormType formType)
        {
            var folderTemplate = GetFolderFormTemplate(folder, formType);

            if (string.IsNullOrEmpty(folderTemplate))
            {
                folderTemplate = GetSchema(folder).GetFormTemplate(formType);
            }
            if (!string.IsNullOrEmpty(folderTemplate))
            {
                var physical = UrlUtility.MapPath(folderTemplate);
                if (!File.Exists(physical))
                {
                    folderTemplate = null;
                }
            }
            return folderTemplate;
        }

        /// <summary>
        /// Gets the folder form template.
        /// </summary>
        /// <param name="textFolder">The text folder.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static string GetFolderFormTemplate(TextFolder textFolder, FormType type)
        {
            string fileVirtualPath = "";
            var folderPath = new FolderPath(textFolder);
            string filePhysicalPath = Path.Combine(folderPath.PhysicalPath, SchemaExtensions.CUSTOM_TEMPLATES, string.Format("{0}.cshtml", type));
            if (File.Exists(filePhysicalPath))
            {
                fileVirtualPath = UrlUtility.Combine(folderPath.VirtualPath, SchemaExtensions.CUSTOM_TEMPLATES, string.Format("{0}.cshtml", type));
            }
            return fileVirtualPath;
        } 
    }
}