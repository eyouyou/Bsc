using System.Globalization;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public class ModuleFormValueProvider : NameValueCollectionValueProvider
    {
        public ModuleFormValueProvider(ControllerContext controllerContext)
            : base(controllerContext.HttpContext.Request.Unvalidated().Form, CultureInfo.CurrentCulture)
        {

        }
    }

    public class ModuleQueryStringValueProvider : NameValueCollectionValueProvider
    {
        public ModuleQueryStringValueProvider(ControllerContext controllerContext)
            : base(controllerContext.HttpContext.Request.QueryString, CultureInfo.CurrentCulture)
        {

        }

    }
}