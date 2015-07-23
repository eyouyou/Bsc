using System;
using System.Web.Mvc;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.View.PositionRender
{
    public class ProxyRenderContext
    {
        public ProxyRenderContext(ControllerContext controllerContext,
            PageRequestContext pageRequestContext, ProxyPosition proxyPosition, string requestPath)
        {
            this.ControllerContext = controllerContext;
            this.PageRequestContext = pageRequestContext;
            this.ProxyPosition = proxyPosition;


            if (string.IsNullOrEmpty(requestPath) && PageRequestContext != null)
            {
                requestPath = pageRequestContext.ModuleUrlContext.GetModuleUrl(proxyPosition.PagePositionId);
            }

            if (string.IsNullOrEmpty(requestPath))
            {
                requestPath = proxyPosition.RequestPath;
            }
            requestPath = requestPath.Trim('~').Trim();

            RequestUri = new Uri(proxyPosition.HostUri, requestPath);

            var httpMethod = controllerContext.HttpContext.Request.HttpMethod;
            if (httpMethod.ToUpper() == "POST" && pageRequestContext != null)
            {
                var postModule = pageRequestContext.AllQueryString[Bsc.Dmtds.Sites.View.ModuleUrlContext.PostModuleParameter];
                if (postModule != proxyPosition.PagePositionId)
                {
                    httpMethod = "GET";
                }
            }
            HttpMethod = httpMethod;
        }
        public ControllerContext ControllerContext { get; private set; }

        public PageRequestContext PageRequestContext { get; private set; }

        public ProxyPosition ProxyPosition { get; set; }

        //public string RequestPath { get; private set; }

        public Uri RequestUri { get; private set; }

        public string HttpMethod { get; private set; }

    }
}