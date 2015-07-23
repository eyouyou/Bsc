﻿using System.Web.Mvc;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.View;
using Bsc.Dmtds.Sites.Web;

namespace Bsc.Dmtds.Sites.Controllers
{
    public class RedirectHelper
    {
        public static ActionResult CreateRedirectResult(Site site, FrontRequestChannel channel, string url, string rawUrl, int? statusCode, RedirectType redirectType, bool appendErrorPath = true)
        {
            var redirectUrl = url;
            if (!UrlUtility.IsAbsoluteUrl(redirectUrl))
            {
                redirectUrl = UrlUtility.ResolveUrl(redirectUrl);
                //WrapperUrl will cause endless loop if site host by ASP.NET development server when transfer redirect.
                if (redirectType != RedirectType.Transfer || Settings.IsHostByIIS)
                {
                    redirectUrl = FrontUrlHelper.WrapperUrl(redirectUrl, site, channel).ToString();
                }
            }
            if (appendErrorPath == true)
            {
                if (!string.IsNullOrEmpty(rawUrl))
                {
                    redirectUrl = redirectUrl.AddQueryParam("returnUrl", rawUrl);
                }
                //if (statusCode != null)
                //{
                //    redirectUrl = redirectUrl.AddQueryParam("statusCode", statusCode.ToString());
                //}
            }
            switch (redirectType)
            {
                case RedirectType.Moved_Permanently_301:
                    return new Redirect301Result(redirectUrl);

                case RedirectType.Transfer:
                    return new TransferResult(redirectUrl, statusCode ?? 200);
                case RedirectType.Found_Redirect_302:
                default:
                    return new RedirectResult(redirectUrl);
            }

        } 
    }
}