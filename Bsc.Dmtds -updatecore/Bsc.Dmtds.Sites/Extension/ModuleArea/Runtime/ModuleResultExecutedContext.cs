using System;
using System.Web.Mvc;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public class ModuleResultExecutedContext : ResultExecutedContext
    {
        public ModuleResultExecutedContext(ControllerContext controllerContext, ActionResult result, bool canceled, Exception exception, string resultHtml) :
            base(controllerContext, result, canceled, exception)
        {
            this.ResultHtml = resultHtml;
        }
        public string ResultHtml { get; private set; }

    }
}