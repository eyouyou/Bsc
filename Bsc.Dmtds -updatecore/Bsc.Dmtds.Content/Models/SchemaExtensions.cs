using System.IO;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content.Models.Paths;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Form;
using Bsc.Dmtds.Form.Html;
using Bsc.Dmtds.Form.Html.Controls;

namespace Bsc.Dmtds.Content.Models
{
    public static class SchemaExtensions
    {
        public static string CUSTOM_TEMPLATES = "CustomTemplates";

        /// <summary>
        /// 生成Schema模板
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="formType">Type of the form.</param>
        /// <returns></returns>
        public static string GenerateForm(this Schema schema, FormType formType)
        {
            ISchema iSchema = schema.AsActual();

            return iSchema.Generate(formType.ToString());
        }
        /// <summary>
        /// 获取Column对应的控件
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static IControl GetControlType(this Column column)
        {
            IControl control = ControlHelper.Resolve(column.ControlType);
            return control;
        }

        #region Template file
        /// <summary>
        /// 取得Schema的模板相对路径
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="formType">Type of the form.</param>
        /// <returns></returns>
        public static string GetFormTemplate(this Schema schema, FormType formType)
        {
            var virtualPath = GetCustomTemplateFileVirtualPath(schema, formType);
            if (string.IsNullOrEmpty(virtualPath))
            {
                virtualPath = GetFormFileVirtualPath(schema, formType);
            }
            return virtualPath;
        }
        private static string GetFormFileVirtualPath(Schema schema, FormType type)
        {
            var razorTemplate = GetFormFilePhysicalPath(schema, type);
            var schemaPath = new SchemaPath(schema);
            var templateVirtualPath = UrlUtility.Combine(schemaPath.VirtualPath, string.Format("{0}.ascx", type));
            if (System.IO.File.Exists(razorTemplate))
            {
                templateVirtualPath = UrlUtility.Combine(schemaPath.VirtualPath, string.Format("{0}.cshtml", type));
            }
            return templateVirtualPath;
        }
        /// <summary>
        /// 取得Schema的模板物理路径
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static string GetFormFilePhysicalPath(this Schema schema, FormType type)
        {
            var schemaPath = new SchemaPath(schema);
            return Path.Combine(schemaPath.PhysicalPath, string.Format("{0}.cshtml", type));
        }
        public static string GetCustomTemplatePhysicalPath(this Schema schema, FormType type)
        {
            var schemaPath = new SchemaPath(schema);
            string filePhysicalPath = Path.Combine(schemaPath.PhysicalPath, CUSTOM_TEMPLATES, string.Format("{0}.cshtml", type));
            return filePhysicalPath;
        }
        private static string GetCustomTemplateFileVirtualPath(Schema schema, FormType type)
        {
            var schemaPath = new SchemaPath(schema);
            string fileVirtualPath = "";
            var filePhysicalPath = GetCustomTemplatePhysicalPath(schema, type);
            if (System.IO.File.Exists(filePhysicalPath))
            {
                fileVirtualPath = UrlUtility.Combine(schemaPath.VirtualPath, CUSTOM_TEMPLATES, string.Format("{0}.cshtml", type));
            }
            return fileVirtualPath;
        }
        #endregion 
    }
}