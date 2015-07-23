using System;
using System.Linq;
using System.Threading;
using System.Web;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core.Persistence.Non_Relational;
using Bsc.Dmtds.Sites.ABTest;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Web
{
    public class FrontHttpRequestWrapper : HttpRequestWrapper
    {
        #region .ctor
        private HttpRequest _request;
        public FrontHttpRequestWrapper(HttpRequest httpRequest)
            : base(httpRequest)
        {
            //applicationPath = base.ApplicationPath;

            //HttpContext.Current.Items["ApplicationPath"] = applicationPath;

            this._request = httpRequest;

            // "~/site1/index"
            appRelativeCurrentExecutionFilePath = base.AppRelativeCurrentExecutionFilePath;

        }
        #endregion

        #region override
        string appRelativeCurrentExecutionFilePath;
        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                return appRelativeCurrentExecutionFilePath;
            }
        }

        public override bool IsSecureConnection
        {
            get
            {
                return IsSSL;
            }
        }
        #endregion

        #region Properties

        private Site _site;
        public Site Site
        {
            get
            {
                return _site;
            }
            private set
            {
                _site = value;
                Site.Current = value;
            }
        }

        public Site RawSite { get; set; }

        public FrontRequestChannel RequestChannel
        {
            get;
            set;
        }
        private string _requestUrl = "";
        public string RequestUrl
        {
            get { return _requestUrl; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var queryQueryIndex = value.IndexOf("?");
                    if (queryQueryIndex > -1)
                    {
                        _requestUrl = value.Substring(0, queryQueryIndex);
                    }
                    else
                    {
                        _requestUrl = value;
                    }
                }
            }
        }
        string sitePath;
        public virtual string SitePath
        {
            get
            {
                return sitePath;
            }
        }

        #region IsSSL
        public virtual bool IsSSL
        {
            get
            {
                if (HttpContext.Current.Items["IsSSL"] == null)
                {
                    return HttpContext.Current.Request.IsSecureConnection;
                }
                else
                {
                    return (bool)HttpContext.Current.Items["IsSSL"];
                }
            }
            set
            {
                HttpContext.Current.Items["IsSSL"] = value;
            }
        }
        #endregion

        #endregion

        #region ResolveSite
        private static bool IgnoreResolveSite(string appRelativeCurrentExecutionFilePath)
        {
            return appRelativeCurrentExecutionFilePath.ToLower().Contains("/cms_data/");
        }
        private static string GetRawHost(HttpRequest request)
        {
            var host = request.Url.Host;
            if (!(HttpContext.Current.Request.Url.Port == 80 || HttpContext.Current.Request.Url.Port == 443))
            {
                host = host + ":" + HttpContext.Current.Request.Url.Port;
            }
            return host;
        }
        internal void ResolveSite()
        {
            if (IgnoreResolveSite(appRelativeCurrentExecutionFilePath))
            {
                return;
            }
            if (!string.IsNullOrEmpty(this.PathInfo))
            {
                appRelativeCurrentExecutionFilePath = appRelativeCurrentExecutionFilePath.TrimEnd('/') + "/" + PathInfo;
            }
            //trim "~/"
            var trimedPath = appRelativeCurrentExecutionFilePath.Substring(2);

            //if the RawUrl is not start with the debug site url.
            //http://www.site1.com/index
            //http://www.site1.com/en/index
            var siteManager = Sites.Services.ServiceFactory.SiteManager;
            if (!trimedPath.StartsWith(SiteHelper.PREFIX_FRONT_DEBUG_URL, StringComparison.InvariantCultureIgnoreCase))
            {
                #region RequestByHostName
                var host = GetRawHost(_request);
                RawSite = siteManager.ResovleSite(new HttpRequestWrapper(_request), host, trimedPath);
                if (RawSite != null)
                {
                    sitePath = RawSite.SitePath;
                    var sitePathLength = 0;
                    if (!string.IsNullOrEmpty(sitePath))
                    {
                        sitePathLength = sitePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        RequestChannel = FrontRequestChannel.HostNPath;
                    }
                    else
                    {
                        RequestChannel = FrontRequestChannel.Host;
                    }
                    var path = trimedPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    RequestUrl = UrlUtility.Combine(new[] { "/" }.Concat(path.Skip(sitePathLength)).ToArray());
                    if (this.Path.EndsWith("/") && !this.RequestUrl.EndsWith("/"))
                    {
                        RequestUrl = RequestUrl + "/";
                    }
                    appRelativeCurrentExecutionFilePath = "~" + RequestUrl;
                }

                #endregion
            }
            else
            {
                #region dev~
                //dev~site1/index
                var path = trimedPath.Split('/');
                var sitePaths = SiteHelper.SplitFullName(path[0].Substring(SiteHelper.PREFIX_FRONT_DEBUG_URL.Count()));

                RawSite = Site.ParseSiteFromRelativePath(sitePaths).AsActual();
                if (RawSite != null)
                {
                    RequestChannel = FrontRequestChannel.Debug;
                }

                RequestUrl = Bsc.Dmtds.Common.Util.UrlUtility.Combine(new[] { "/" }.Concat(path.Skip(1)).ToArray());
                if (this.Path.EndsWith("/") && !this.RequestUrl.EndsWith("/"))
                {
                    RequestUrl = RequestUrl + "/";
                }
                appRelativeCurrentExecutionFilePath = "~" + RequestUrl;
                #endregion
            }

            if (RawSite != null)
            {
                if (RequestChannel == FrontRequestChannel.Debug || RequestChannel == FrontRequestChannel.Host || RequestChannel == FrontRequestChannel.HostNPath)
                {
                    Site = MatchSiteByVisitRule(RawSite);
                }

                //set current site repository
                Repository.Current = Site.GetRepository();

                if (!string.IsNullOrEmpty(Site.Culture))
                {
                    var culture = Bsc.Dmtds.Core.CultureInfoHelper.CreateCultureInfo(Site.Culture);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }

                IsSSL = DetectSSLRequest(Site, _request);
            }

            //decode the request url. for chinese character
            this.RequestUrl = HttpUtility.UrlDecode(this.RequestUrl);
        }
        protected bool DetectSSLRequest(Site site, HttpRequest httpRequest)
        {
            var isSSL = httpRequest.IsSecureConnection;
            if (isSSL == false)
            {
                var sslDetection = site.SSLDetection;
                if (sslDetection != null && !string.IsNullOrEmpty(sslDetection.Key))
                {
                    var value = httpRequest.Headers[sslDetection.Key];
                    if (string.IsNullOrEmpty(value))
                    {
                        value = httpRequest.QueryString[sslDetection.Key];
                    }
                    isSSL = sslDetection.Value.EqualsOrNullEmpty(value, StringComparison.OrdinalIgnoreCase);
                }
            }
            return isSSL;
        }
        protected virtual Site MatchSiteByVisitRule(Site site)
        {
            ABSiteSetting abSiteSetting = null;
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var matchedSite = Bsc.Dmtds.Core.Runtime.EngineContext.Current.Resolve<Services.ABSiteSettingManager>().MatchRule(site, httpContext, out abSiteSetting);
            if (matchedSite != site && abSiteSetting != null && abSiteSetting.RedirectType != null && abSiteSetting.RedirectType.Value != RedirectType.Transfer)
            {
                string url = null;
                var rawUrl = RequestUrl;
                if (!string.IsNullOrEmpty(httpContext.Request.Url.Query))
                {
                    rawUrl = rawUrl + httpContext.Request.Url.Query;
                }
                if (this.RequestChannel == FrontRequestChannel.Debug)
                {
                    url = FrontUrlHelper.WrapperUrl(rawUrl, matchedSite, FrontRequestChannel.Debug).ToString();
                }
                else
                {
                    var domain = matchedSite.FullDomains.FirstOrDefault();
                    if (!string.IsNullOrEmpty(domain))
                    {
                        var baseUri = httpContext.Request.Url.Scheme + "://" + domain;
                        url = new Uri(new Uri(baseUri), rawUrl).ToString();
                    }
                }
                if (!string.IsNullOrEmpty(url))
                {
                    if (abSiteSetting.RedirectType.Value == RedirectType.Found_Redirect_302)
                    {
                        httpContext.Response.Redirect(url);
                    }
                    else if (abSiteSetting.RedirectType.Value == RedirectType.Moved_Permanently_301)
                    {
                        httpContext.Response.RedirectPermanent(url);
                    }
                }
            }
            return matchedSite;
        }
        #endregion
    }
}