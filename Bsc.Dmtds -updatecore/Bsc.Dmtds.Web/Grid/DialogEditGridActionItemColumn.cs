using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;
using Bsc.Dmtds.Core.Mvc.Routing;
using Bsc.Dmtds.Core.Persistence.Non_Relational;


namespace Bsc.Dmtds.Web.Grid
{
    public class DialogEditGridActionItemColumn : GridItemColumn
    {
        public DialogEditGridActionItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue) 
            : base(gridColumn, dataItem, propertyValue)
        {
        }
        public override IHtmlString RenderItemColumn(ViewContext viewContext)
        {
            if (DataItem is IIdentifiable)
            {
                var data = (IIdentifiable)DataItem;
                var linkText = "编辑";
                var @class = Class;
                if (!string.IsNullOrEmpty(this.GridColumn.PropertyName))
                {
                    linkText = this.PropertyValue == null ? "" : PropertyValue.ToString();
                }
                else
                {
                    @class = "o-icon edit " + @class;
                }
                return viewContext.HtmlHelper().ActionLink(linkText, "Edit", viewContext.RequestContext.AllRouteValues().Merge("UUID", data.UUID), new Dictionary<string, object> { { "class", @class } });
            }

            return new HtmlString("");

        }
        protected virtual string Class
        {
            get
            {
                return "dialog-link";
            }
        }
    }
}