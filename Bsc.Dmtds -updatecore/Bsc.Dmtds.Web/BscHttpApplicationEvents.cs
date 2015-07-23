using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Collection;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Mvc;
using Bsc.Dmtds.Core.Mvc.Routing;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Web.Models;

namespace Bsc.Dmtds.Web
{
    [Dependency(typeof(IHttpApplicationEvents), Key = "BscHttpApplicationEvents")]
    public class BscHttpApplicationEvents : MvcModule
    {
        #region CustomRazorViewEngine
        /// <summary>
        /// Allow the area to use the global shared view .
        /// </summary>
        private class CustomRazorViewEngine : RazorViewEngine
        {
            public CustomRazorViewEngine()
                : base()
            {
                var baseDir = EngineContext.Current.Resolve<IBaseDir>();

                base.AreaMasterLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/" + baseDir.DataPathName + "/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml" }; //add: "~/Views/Shared/{0}.cshtml" 

                base.AreaViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/" + baseDir.DataPathName + "/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };//add: "~/Views/Shared/{0}.cshtml"

                base.AreaPartialViewLocationFormats = base.AreaViewLocationFormats;
            }
        }
        #endregion

        #region RegisterRoutes
        public static void RegisterRoutes(RouteCollection routes)
        {
//            routes.Add(new Route("api/metaweblog", new RouteValueDictionary(new { controller = "nonexistingcontroller" }), new RouteValueDictionary(new { controller = "nonexistingcontroller" }),
//     new Kooboo.CMS.Web.Interoperability.MetaWeblog.MetaWeblogRouteHandler()));

            RouteTableRegister.RegisterRoutes(routes);


            ModelMetadataProviders.Current = new BscDataAnnotationsModelMetadataProvider();

            ModelValidatorProviders.Providers.RemoveAt(0);
            ModelValidatorProviders.Providers.Insert(0, new BscDataAnnotationsModelValidatorProvider());

            //Job.Jobs.Instance.AttachJob("test", new Kooboo.Job.TestJob(), 1000, null, true);
        }
        #endregion

        #region Application_Start
        public override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);

            //execute the initializer method.
//            ApplicationInitialization.Execute();
//            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
//            ControllerBuilder.Current.SetControllerFactory();
            //
//            ControllerBuilder.Current.SetControllerFactory(new Kooboo.CMS.Sites.CMSControllerFactory());

            //ViewEngine for module.            
//            ViewEngines.Engines.Insert(0, new Kooboo.CMS.Sites.Extension.ModuleArea.Runtime.ModuleRazorViewEngine());
//            ViewEngines.Engines.Insert(1, new Kooboo.CMS.Sites.Extension.ModuleArea.Runtime.ModuleWebFormViewEngine());
            ViewEngines.Engines.Insert(0, new CustomRazorViewEngine());


            AreaRegistration.RegisterAllAreas();

            #region Binders
            ModelBinders.Binders.DefaultBinder = new Json_netModelBinder();

            ModelBinders.Binders.Add(typeof(DynamicDictionary), new DynamicDictionaryBinder());
//            ModelBinders.Binders.Add(typeof(Kooboo.CMS.Sites.DataRule.IDataRule), new Kooboo.CMS.Web.Areas.Sites.ModelBinders.DataRuleBinder());
            //ModelBinders.Binders.Add(typeof(Kooboo.CMS.Sites.DataRule.DataRuleBase), new Kooboo.CMS.Web.Areas.Sites.ModelBinders.DataRuleBinder());
            //ModelBinders.Binders.Add(typeof(Kooboo.CMS.Sites.DataRule.HttpDataRule), new Kooboo.CMS.Web.Areas.Sites.ModelBinders.DataRuleBinder());

//            ModelBinders.Binders.Add(typeof(Kooboo.CMS.Sites.Models.PagePosition), new Kooboo.CMS.Web.Areas.Sites.ModelBinders.PagePositionBinder());
//            ModelBinders.Binders.Add(typeof(Kooboo.CMS.Sites.Models.Parameter), new Kooboo.CMS.Web.Areas.Sites.ModelBinders.ParameterBinder());
            #endregion

            RegisterRoutes(RouteTable.Routes);

//
//            Kooboo.Web.Mvc.Menu.MenuFactory.RegisterAreaMenu("AreasMenu", Path.Combine(Settings.BaseDirectory, "Menu.config"));
//
//            Kooboo.CMS.Content.Persistence.Providers.RepositoryProvider.TestDbConnection();
        }
        #endregion
    }
}