﻿using System;
using System.IO;
using System.Web;
using Bsc.Dmtds.Common;
using System.Web.Mvc;
using System.Web.Routing;
using Bsc.Dmtds.Core.Web;
using Bsc.Dmtds.Sites.Web;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public static class ModuleActionResultExecutor
    {
        private class ActionResultExecutorHttpResponse : HttpResponseBaseWrapper
        {
            public ActionResultExecutorHttpResponse(HttpResponseBase httpResponse)
                : base(httpResponse)
            {

            }
            private TextWriter output;
            public override System.IO.TextWriter Output
            {
                get
                {
                    if (output != null)
                    {
                        return output;
                    }
                    return base.Output;
                }
                set
                {
                    output = value;
                }
            }
        }
        private class ActionResultExecutorHttpContext : HttpContextBaseWrapper
        {
            public ActionResultExecutorHttpContext(HttpContextBase httpContext)
                : base(httpContext)
            {

            }
            private HttpResponseBase httpResponse = null;
            public override HttpResponseBase Response
            {
                get
                {
                    if (httpResponse == null)
                    {
                        httpResponse = new ActionResultExecutorHttpResponse(base.Response);
                    }
                    return httpResponse;
                }
            }
        }
        private class RedirectToRouteResultWrapper : RedirectToRouteResult
        {
            public RedirectToRouteResultWrapper(RouteValueDictionary routeValues)
                : base(routeValues)
            {

            }

            public RedirectToRouteResultWrapper(string routeName, RouteValueDictionary routeValues)
                : base(routeName, routeValues)
            {

            }
            public RedirectToRouteResultWrapper(RedirectToRouteResult redirectToRouteResult, RouteCollection routeTable) :
                base(redirectToRouteResult.RouteName, redirectToRouteResult.RouteValues)
            {
                this.Routes = routeTable;
            }
            private RouteCollection _routes;
            internal RouteCollection Routes
            {
                get
                {
                    if (this._routes == null)
                    {
                        this._routes = RouteTable.Routes;
                    }
                    return this._routes;
                }
                set
                {
                    this._routes = value;
                }
            }
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                if (context.IsChildAction)
                {
                    throw new InvalidOperationException(SR.GetString("RedirectAction_CannotRedirectInChildAction"));
                }
                string str = UrlHelper.GenerateUrl(this.RouteName, null, null, this.RouteValues, this.Routes, context.RequestContext, false);
                if (string.IsNullOrEmpty(str))
                {
                    throw new InvalidOperationException(SR.GetString("Common_NoRouteMatched"));
                }
                context.Controller.TempData.Keep();
                context.HttpContext.Response.Redirect(str, false);

            }
        }
        public static bool IsExclusiveResult(ActionResult actionResult)
        {
            return actionResult is FileResult
                || actionResult is PartialViewResult
                || actionResult is JsonResult
                || actionResult is ContentResult
                || actionResult is JavaScriptResult
                || actionResult is RedirectToRouteResult;
        }
        public static void ExecuteExclusiveResult(ControllerContext controllerContext, ActionResult actionResult)
        {
            if (IsExclusiveResult(actionResult))
            {
                controllerContext.HttpContext.Response.RestoreRawOutput();

                if (actionResult is RedirectToRouteResult)
                {
                    actionResult = new RedirectToRouteResultWrapper((RedirectToRouteResult)actionResult, ((ModuleRequestContext)controllerContext.RequestContext).ModuleContext.FrontEndContext.RouteTable);
                }

                controllerContext.HttpContext.Response.Clear();
                controllerContext.HttpContext.Response.ClearContent();
                actionResult.ExecuteResult(controllerContext);
                controllerContext.HttpContext.Response.End();
            }
        }
        public static string ExecuteResult(ControllerContext controllerContext, ActionResult actionResult)
        {
            if (IsExclusiveResult(actionResult))
            {
                ExecuteExclusiveResult(controllerContext, actionResult);
                return string.Empty;
            }
            else
            {
                using (StringWriter textWriter = new StringWriter())
                {
                    controllerContext.HttpContext = new ActionResultExecutorHttpContext(controllerContext.HttpContext);
                    ((ActionResultExecutorHttpResponse)controllerContext.HttpContext.Response).Output = textWriter;
                    actionResult.ExecuteResult(controllerContext);
                    //reset HttpContext
                    controllerContext.HttpContext = null;

                    return textWriter.ToString();
                }
            }
        }
    }
}