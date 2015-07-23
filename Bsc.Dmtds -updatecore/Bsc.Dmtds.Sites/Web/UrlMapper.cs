using System;
using System.Text.RegularExpressions;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Web
{
    public interface IUrlMapper
    {
        bool Map(Site site, string inputUrl, out string outputUrl, out RedirectType redirectType);
 
    }
    public static class UrlMapperFactory
    {
        static UrlMapperFactory()
        {
            Default = new DefaultUrlMapper();
        }
        public static IUrlMapper Default { get; set; }
    }
    public class DefaultUrlMapper : IUrlMapper
    {
        #region IUrlMapper Members

        public bool Map(Site site, string inputUrl, out string outputUrl, out RedirectType redirectType)
        {
            outputUrl = string.Empty;
            redirectType = RedirectType.Found_Redirect_302;
            if (string.IsNullOrEmpty(inputUrl))
            {
                return false;
            }
            var mapSettings = Services.ServiceFactory.UrlRedirectManager.All(site, "");
            //inputUrl = inputUrl.Trim('/');
            if (!inputUrl.StartsWith("/"))
            {
                inputUrl = "/" + inputUrl;
            }
            foreach (var setting in mapSettings)
            {
                var inputPattern = setting.InputUrl;//.Trim('/');
                if (setting.Regex)
                {
                    try
                    {
                        if (Regex.IsMatch(inputUrl, inputPattern, RegexOptions.IgnoreCase))
                        {
                            outputUrl = Regex.Replace(inputUrl, inputPattern, setting.OutputUrl, RegexOptions.IgnoreCase);
                            redirectType = setting.RedirectType;
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Bsc.Dmtds.Common.HealthMonitoring.Log.LogException(e);
                    }

                }
                else
                {
                    if (inputUrl.EqualsOrNullEmpty(inputPattern, StringComparison.CurrentCultureIgnoreCase))
                    {
                        outputUrl = setting.OutputUrl;
                        redirectType = setting.RedirectType;
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}