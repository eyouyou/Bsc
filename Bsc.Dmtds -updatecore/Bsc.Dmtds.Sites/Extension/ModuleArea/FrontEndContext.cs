using System.Web.Routing;
using Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public class FrontEndContext
    {
        string _moduleName;
        ModuleSettings _moduleSettings;
        ModulePosition _modulePosition;
        public FrontEndContext(string moduleName, ModuleSettings moduleSettings, ModulePosition modulePosition)
        {
            this._moduleName = moduleName;
            this._moduleSettings = moduleSettings;
            this._modulePosition = modulePosition;
        }
        #region RouteTable
        public RouteCollection RouteTable
        {
            get
            {
                return RouteTables.GetRouteTable(this._moduleName);
            }
        }
        #endregion

        #region ModuleSettings
        public ModuleSettings ModuleSettings
        {
            get
            {
                return this._moduleSettings;
            }
        }
        #endregion

        #region ModulePosition
        public ModulePosition ModulePosition
        {
            get
            {
                return _modulePosition;
            }
        }
        #endregion

        #region EnableTheme
        private bool enableTheme = true;
        public virtual bool EnableTheme
        {
            get
            {
                return enableTheme;
            }
            set
            {
                this.enableTheme = value;
            }
        }
        #endregion

        #region EnableScript
        private bool enableScript = true;
        public virtual bool EnableScript
        {
            get
            {
                return enableScript;
            }
            set
            {
                enableScript = value;
            }
        }
        #endregion
 
    }
}