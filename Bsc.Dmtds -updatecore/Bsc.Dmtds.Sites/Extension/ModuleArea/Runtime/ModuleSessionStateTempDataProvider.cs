using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public class ModuleSessionStateTempDataProvider : ITempDataProvider
    {
        // Fields
        internal const string TempDataSessionStateKey = "__ModuleControllerTempData";

        // Methods
        public virtual IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            HttpContextBase httpContext = controllerContext.HttpContext;
            if (httpContext.Session == null)
            {
                throw new InvalidOperationException("SessionStateTempDataProvider_SessionStateDisabled");
            }
            Dictionary<string, object> dictionary = httpContext.Session[TempDataSessionStateKey] as Dictionary<string, object>;
            if (dictionary != null)
            {
                httpContext.Session.Remove(TempDataSessionStateKey);
                return dictionary;
            }
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public virtual void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            HttpContextBase httpContext = controllerContext.HttpContext;
            if (httpContext.Session == null)
            {
                throw new InvalidOperationException("SessionStateTempDataProvider_SessionStateDisabled");
            }
            httpContext.Session[TempDataSessionStateKey] = values;
        }
    }
}