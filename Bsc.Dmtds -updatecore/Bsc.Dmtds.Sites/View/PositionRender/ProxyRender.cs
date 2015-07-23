using System;
using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Caching;
using Bsc.Dmtds.Sites.Caching;
using Bsc.Dmtds.Sites.View.WebProxy;

namespace Bsc.Dmtds.Sites.View.PositionRender
{
    public class ProxyRender
    {
        #region ProxyRender
        IWebProxy _webProxy;

        public ProxyRender(IWebProxy webProxy)
        {
            _webProxy = webProxy;

        }
        #endregion

        #region Render
        public virtual IHtmlString Render(ProxyRenderContext proxyRenderContext)
        {
            var positionId = proxyRenderContext.ProxyPosition.PagePositionId;

            Func<IHtmlString> getHtml = () =>
            {
                var html = _webProxy.ProcessRequest(proxyRenderContext);

                return html;
            };
            var cacheSetting = proxyRenderContext.ProxyPosition.OutputCache;
            if (cacheSetting != null && cacheSetting.EnableCaching != null && cacheSetting.EnableCaching == true && proxyRenderContext.HttpMethod.ToUpper() == "GET")
            {
                string cacheKey = string.Format("{0}||{1}||{2}", positionId, proxyRenderContext.RequestUri.ToString(), proxyRenderContext.ProxyPosition.NoProxy);
                return proxyRenderContext.PageRequestContext.Site.ObjectCache().GetCache(cacheKey, getHtml, cacheSetting.ToCachePolicy());
            }
            else
            {
                return getHtml();
            }

        }
        #endregion

        #region TryRemoteRequest
        public virtual ActionResult TryRemoteRequest(ControllerContext controllerContext)
        {
            var hasRemoteProxy = controllerContext.HttpContext.Request.QueryString["hasRemoteProxy"];

            if (!string.IsNullOrEmpty(hasRemoteProxy) && hasRemoteProxy.ToLower() == "true")
            {
                return new RemoteRequestActionResult(this);
            }
            return null;
        }
        #endregion 
    }
}