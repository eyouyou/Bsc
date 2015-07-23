using System;
using System.Web.Mvc;
using Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public static class ModuleControllerContextExtensions
    {
        #region GetModuleName
        /// <summary>
        /// Get the module name from controller context.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public static string GetModuleName(this ControllerContext controllerContext)
        {
            return Bsc.Dmtds.Core.Mvc.AreaHelpers.GetAreaName(controllerContext.RouteData);
        }
        #endregion

        #region GetModuleContext
        public static ModuleContext GetModuleContext(this ControllerContext controllerContext)
        {
            CheckFrontendController(controllerContext);
            return ((ModuleRequestContext)(controllerContext.RequestContext)).ModuleContext;
        }

        private static void CheckFrontendController(ControllerContext controllerContext)
        {
            if (!(controllerContext.RequestContext is ModuleRequestContext))
            {
                throw new InvalidOperationException("The GetModuleContext only can be invoked from the module frontend controller.");
            }
        }
        #endregion

        #region ShareData
        public static void ShareData(this ControllerContext controllerContext, string key, object data)
        {
            CheckFrontendController(controllerContext);
            Page_Context.Current.ControllerContext.Controller.ViewData[key] = data;
        }
        public static object GetSharedData(this ControllerContext controllerContext, string key)
        {
            CheckFrontendController(controllerContext);

            if (Page_Context.Current.ControllerContext.Controller.ViewData.ContainsKey(key))
            {
                return Page_Context.Current.ControllerContext.Controller.ViewData[key];
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}