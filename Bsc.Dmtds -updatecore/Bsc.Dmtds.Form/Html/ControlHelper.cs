﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Form.Html.Controls;
using CheckBox = Bsc.Dmtds.Form.Html.Controls.CheckBox;
using File = Bsc.Dmtds.Form.Html.Controls.File;
using TextBox = Bsc.Dmtds.Form.Html.Controls.TextBox;

namespace Bsc.Dmtds.Form.Html
{
    public static class ControlHelper
    {
        #region Static
        static KeyValuePair<string, string>[] TemplateDirs = new KeyValuePair<string, string>[0];

        static IDictionary<string, Controls.IControl> controls = new Dictionary<string, IControl>(StringComparer.CurrentCultureIgnoreCase);

        static ControlHelper()
        {
            var baseDir = EngineContext.Current.Resolve<IBaseDir>();
            TemplateDirs = new[] { new KeyValuePair<string,string>(Path.Combine(baseDir.DataPhysicalPath, "Views", "ControlTypes"),UrlUtility.Combine(baseDir.DataVirtualPath, "Views", "ControlTypes")),
                new KeyValuePair<string,string>(Path.Combine(baseDir.DataPhysicalPath, "ContentType_Templates", "Controls"),UrlUtility.Combine(baseDir.DataVirtualPath, "ContentType_Templates", "Controls")) };

            RegisterControl(new TextBox());
            RegisterControl(new InputInt32());
            RegisterControl(new InputFloat());
            RegisterControl(new CheckBox());
            RegisterControl(new Date());
            RegisterControl(new File());
            //RegisterControl(new ImageCrop());
            RegisterControl(new Display());
            RegisterControl(new Hidden());
            RegisterControl(new Empty());
            RegisterControl(new DropDownList());
            RegisterControl(new CheckBoxList());
            RegisterControl(new RadioList());
            //RegisterControl(new MultiFiles());
            RegisterControl(new TextArea());
            RegisterControl(new Tinymce());
            RegisterControl(new HighlightEditor());
            RegisterControl(new Password());
            //RegisterControl(new InputNumber());
            //RegisterControl(new CLEditor());
        }
        #endregion
        #region RegisterControl
        public static void RegisterControl(IControl control)
        {
            controls[control.Name] = control;
        }

        public static bool Contains(string controlType)
        {
            return ResolveAll().Where(it => string.Compare(it.Name, controlType, true) == 0).Count() > 0;
        }
        #endregion

        #region Resolve
        public static IControl Resolve(string controlType)
        {
            IControl control = null;
            if (!string.IsNullOrEmpty(controlType) && controls.ContainsKey(controlType))
            {
                control = controls[controlType];
            }
            return control;
        }
        #endregion

        #region ResolveAll
        public static IEnumerable<ControlMetadata> ResolveAll()
        {
            var controlNames = controls.Values.Select(it => new ControlMetadata() { Name = it.Name, DataType = it.DataType });

            var controlViews = ResolveFromViews();
            controlNames = controlNames.Concat(controlViews)
               .Distinct(new ControlMetadataComparer());

            return controlNames;
        }
        private static IEnumerable<ControlMetadata> ResolveFromViews()
        {
            var controls = new List<string>();
            foreach (var dir in TemplateDirs)
            {
                if (Directory.Exists(dir.Key))
                {
                    foreach (var file in Directory.EnumerateFiles(dir.Key, "*.cshtml"))
                    {
                        var name = Path.GetFileNameWithoutExtension(file);
                        if (controls.Count(it => it.EqualsOrNullEmpty(name, StringComparison.OrdinalIgnoreCase)) == 0)
                        {
                            controls.Add(name);
                        }
                    }
                }
            }
            return controls.Select(it => new ControlMetadata() { Name = it, DataType = null }); ;
        }
        #endregion

        #region Render
        static string AllowedEditWraper(string template)
        {
            var result = string.Empty;
            result = string.Format(@"
            @if (allowedEdit) {{
                {0}
            }}", template);
            return result;
        }


        public static string Render(this IColumn column, ISchema schema, bool isUpdate)
        {
            var controlType = column.ControlType;
            if (isUpdate && !column.Modifiable)
            {
                controlType = "Hidden";
            }
            if (string.IsNullOrEmpty(controlType))
            {
                return string.Empty;
            }
            if (!Contains(controlType))
            {
                throw new Exception(string.Format("Control type {0} does not exists.", controlType));
            }
            bool rendered = false;
            string controlHtml = ProcessRazorView(schema, column, out rendered);
            if (!rendered && controls.ContainsKey(controlType))
            {
                controlHtml = controls[controlType].Render(schema, column);
            }

            if (string.Equals(column.Name, "published", StringComparison.OrdinalIgnoreCase))
            {
                controlHtml = AllowedEditWraper(controlHtml);
            }

            return controlHtml;
        }
        #endregion

        #region Razor view
        private static string GetControlViewPhysicalPath(string controlType)
        {
            foreach (var dir in TemplateDirs)
            {
                var path = Path.Combine(dir.Key, controlType + ".cshtml");
                if (System.IO.File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }
        private static string GetControlViewVirtualPath(string controlType)
        {
            foreach (var dir in TemplateDirs)
            {
                var path = Path.Combine(dir.Key, controlType + ".cshtml");
                if (System.IO.File.Exists(path))
                {
                    return UrlUtility.Combine(dir.Value, controlType + ".cshtml");
                }
            }
            return null;
        }
        public static string ProcessRazorView(ISchema schema, IColumn column, out bool rendered)
        {
            string controlFileVirutalPath = GetControlViewVirtualPath(column.ControlType);
            rendered = false;
            if (!string.IsNullOrEmpty(controlFileVirutalPath))
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["ControllerContext"] != null)
                {
                    var controllerContext = (ControllerContext)HttpContext.Current.Items["ControllerContext"];
                    var viewData = new ViewDataDictionary() { Model = column };
                    viewData["Schema"] = schema;
                    rendered = true;
                    return RazorViewHelper.RenderView(controlFileVirutalPath, controllerContext, viewData);
                }
            }

            return "";
        }
        #endregion
    }
}