using System;
using System.Web.Mvc;
using System.Web.Routing;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core.Mvc;

namespace Bsc.Dmtds.Web.Models
{
    public class RemoteExAttribute : RemoteAttribute
    {
         public RemoteExAttribute(string routeName)
            : base(routeName)
        {
        }
        public RemoteExAttribute(string action, string controller)
            : this(action, controller, null)
        {
        }
        public RemoteExAttribute(string action, string controller, string areaName)
            : base(action, string.IsNullOrEmpty(controller) ? "*" : controller, areaName)
        {
            if (string.IsNullOrEmpty(controller) || controller == "*")
            {
                variableController = true;
            }
            this.HttpMethod = "POST";
        }
        private bool variableController = false;
        private string routeFields;
        private string[] routeFieldsSplit = new string[0];
        public string RouteFields
        {
            get
            {
                return routeFields;
            }
            set
            {
                routeFields = value;
                if (!string.IsNullOrEmpty(routeFields))
                {
                    routeFieldsSplit = RouteFields.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    routeFieldsSplit = new string[0];
                }

            }
        }

        protected override string GetUrl(ControllerContext controllerContext)
        {
            var routeData = new RouteValueDictionary(this.RouteData);
            if (variableController)
            {
                routeData["controller"] = controllerContext.RouteData.Values["controller"];
            }
            //merge the route data
            if (routeFieldsSplit.Length > 0)
            {
                foreach (var field in routeFieldsSplit)
                {
                    routeData[field] = controllerContext.RequestContext.GetRequestValue(field);
                }
            }
            VirtualPathData data = this.Routes.GetVirtualPathForArea(controllerContext.RequestContext, this.RouteName,
                routeData);
            if (data == null)
            {
                throw new InvalidOperationException(SR.GetString("RemoteAttribute_NoUrlFound"));
            }
            return data.VirtualPath;

            //return base.GetUrl(controllerContext);
        }
    }
}