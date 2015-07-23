using System.Linq;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.View.PositionRender
{
    public class RemoteRequestActionResult : ActionResult
    {
        #region .ctor
        ProxyRender _proxyRender;
        public RemoteRequestActionResult(ProxyRender proxyRender)
        {
            _proxyRender = proxyRender;
        }
        #endregion
        public override void ExecuteResult(ControllerContext context)
        {
            string siteName = context.HttpContext.Request.QueryString["cms_siteName"];
            string pageName = context.HttpContext.Request.QueryString["cms_pageName"];

            if (string.IsNullOrEmpty(siteName) || string.IsNullOrEmpty(pageName))
            {
                context.HttpContext.Response.StatusCode = 404;
                return;
            }
            var site = new Site(siteName).AsActual();
            if (site == null)
            {
                context.HttpContext.Response.StatusCode = 404;
                return;
            }
            var page = new Page(site, pageName).AsActual();
            if (page == null || page.PagePositions == null)
            {
                context.HttpContext.Response.StatusCode = 404;
                return;
            }
            var proxyPosition = page.PagePositions.OfType<ProxyPosition>().FirstOrDefault();
            if (proxyPosition == null)
            {
                context.HttpContext.Response.StatusCode = 404;
                return;
            }
            var remoteUrl = context.HttpContext.Request.RawUrl;
            var content = _proxyRender.Render(new ProxyRenderContext(context, null, proxyPosition, remoteUrl));
            if (content != null && !string.IsNullOrEmpty(content.ToString()))
            {
                context.HttpContext.Response.Write(content.ToString());
            }
        }
    }
}