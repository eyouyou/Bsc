using System;
using System.Web.Mvc;
using Bsc.Dmtds.Common.Collection;
using Bsc.Dmtds.Core.Reflection;
using IValueProvider = Bsc.Dmtds.Core.Formula.IValueProvider;

namespace Bsc.Dmtds.Sites.View
{
    public class PageSettingValueProvider:IValueProvider
    {
        Page_Context _pageContext;
        public PageSettingValueProvider(Page_Context pageContext)
        {
            this._pageContext = pageContext;
        }
        public object GetValue(string fieldName)
        {
            object value = GetFieldValueFromViewData(_pageContext.ControllerContext.Controller.ViewData, fieldName);
            if (value == null)
            {
                foreach (var item in _pageContext.PositionsViewData.Values)
                {
                    value = GetFieldValueFromViewData(item, fieldName);
                    if (value != null)
                    {
                        break;
                    }
                }
            }
            // if the value can not be found in ViewData, will try to get it from QueryString and page route data.
            if (value == null)
            {
                value = _pageContext.PageRequestContext.AllQueryString[fieldName];
            }
            return value;
        }
        private object GetFieldValueFromViewData(ViewDataDictionary viewData, string fieldName)
        {
            object value = null;
            string viewDataKey = null;
            fieldName = ParseFieldName(fieldName, out viewDataKey);
            if (string.IsNullOrEmpty(viewDataKey))
            {
                if (viewData.ContainsKey(fieldName))
                {
                    value = viewData[fieldName];
                }
                if (value == null && viewData.Model != null)
                {
                    value = GetFieldValueFromObject(viewData.Model, fieldName);
                }
                if (value == null)
                {
                    foreach (var item in viewData.Values)
                    {
                        value = GetFieldValueFromObject(item, fieldName);
                        if (value != null)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                if (viewData.ContainsKey(viewDataKey))
                {
                    value = GetFieldValueFromObject(viewData[viewDataKey], fieldName);
                }
            }
            return value;

        }
        private string ParseFieldName(string fieldName, out string viewDataKey)
        {
            var pointIndex = fieldName.IndexOf('.');
            viewDataKey = null;
            if (pointIndex != -1)
            {
                viewDataKey = fieldName.Substring(0, pointIndex);
                if (pointIndex + 1 <= fieldName.Length)
                {
                    fieldName = fieldName.Substring(pointIndex + 1);
                }
            }
            return fieldName;
        }
        private object GetFieldValueFromObject(Object o, string fieldName)
        {
            if (o is DynamicDictionary)
            {
                return ((DynamicDictionary)o)[fieldName];
            }
            try
            {
                return o.Members().Properties[fieldName];
            }
            catch
            {
                return null;
            }
        }

    }
}