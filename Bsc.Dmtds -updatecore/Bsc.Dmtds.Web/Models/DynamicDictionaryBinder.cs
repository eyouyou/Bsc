using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using Bsc.Dmtds.Common.Collection;

namespace Bsc.Dmtds.Web.Models
{
    public class DynamicDictionaryBinder : IModelBinder
    {
        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            DynamicDictionary dic = null;
            if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                dic = new DynamicDictionary();
                string prefix = bindingContext.ModelName + ".";
                var form = controllerContext.RequestContext.HttpContext.Request.Unvalidated().Form;
                foreach (var item in form.AllKeys)
                {
                    if (item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        var fieldName = item.Substring(prefix.Length);
                        var value = form[item];
                        if (value == "true,false")
                        {
                            value = "true";
                        }
                        dic[fieldName] = value;
                    }
                }
            }
            return dic;
        }
    }

    public class StringDictionaryBinder : System.Web.Mvc.IModelBinder
    {
        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            Dictionary<string, string> dic = null;
            if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                dic = new Dictionary<string, string>();
                string prefix = bindingContext.ModelName + ".";
                var form = controllerContext.RequestContext.HttpContext.Request.Unvalidated().Form;
                foreach (var item in form.AllKeys)
                {
                    if (item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        var fieldName = item.Substring(prefix.Length);
                        var value = form[item];
                        if (value == "true,false")
                        {
                            value = "true";
                        }
                        dic[fieldName] = value;
                    }
                }
            }
            return dic;
        }
    }
}