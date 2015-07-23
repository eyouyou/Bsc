using System.IO;
using System.Web.Mvc;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Core;

namespace Bsc.Dmtds.Web.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistrationEx 
    {
        public override string AreaName 
        {
            get 
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                new[] { "Bsc.Bmtds.Web.Areas.Account.Controllers", "Bsc.Dmtds.Core.Mvc.WebResourceLoader",}
            );
            Bsc.Dmtds.Core.Mvc.WebResourceLoader.ConfigurationManager.RegisterSection(AreaName, Path.Combine(Settings.BaseDirectory, "Areas", AreaName, "WebResources.config"));

            base.RegisterArea(context);

        }
    }
}