using System.Web.Mvc;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public class ModuleActionInvokedContext
    {
        public ModuleActionInvokedContext(ModulePosition modulePosition, ControllerContext controllerContext, ActionResult actionResult)
        {
            this.ControllerContext = controllerContext;
            this.ModulePosition = modulePosition;
            this.ActionResult = actionResult;
        }
        public ControllerContext ControllerContext { get; private set; }
        public ModulePosition ModulePosition { get; private set; }
        public virtual ActionResult ActionResult { get; private set; }
    }
}