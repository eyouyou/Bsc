﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Bsc.Dmtds.Core.Mvc
{
    public static  class RequestContextExtensions
    {
        /// <summary>
        /// Alls the route values and query string value in current request.
        /// </summary>
        /// <param name="requestContext">The controller.</param>
        /// <returns></returns>
        public static RouteValueDictionary AllRouteValues(this RequestContext requestContext)
        {
            RouteValueDictionary values = new RouteValueDictionary(requestContext.RouteData.Values);
            foreach (string key in requestContext.HttpContext.Request.QueryString.Keys)
            {
                values[key] = requestContext.HttpContext.Request.QueryString[key];
            }
            return values;
        }

        public static string GetRequestValue(this RequestContext requestContext, string name)
        {
            if (requestContext.RouteData.Values[name] != null)
            {
                return requestContext.RouteData.Values[name].ToString();
            }
            else
            {
                return requestContext.HttpContext.Request[name];
            }
        }

        /// <summary>
        /// URLs the helper.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        public static UrlHelper UrlHelper(this RequestContext requestContext)
        {
            return new UrlHelper(requestContext);
        } 
    }
}