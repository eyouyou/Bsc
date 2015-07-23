using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;
using Bsc.Dmtds.Core.Mvc.Routing;
using Bsc.Dmtds.Core.Persistence.Non_Relational;


namespace Bsc.Dmtds.Web.Grid
{
    public class EditGridActionItemColumn : GridItemColumn
    {
        public EditGridActionItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue)
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
                var url = viewContext.UrlHelper().Action("Edit"
                    , viewContext.RequestContext.AllRouteValues().Merge("UUID", data.UUID).Merge("return", viewContext.HttpContext.Request.RawUrl));

                return new HtmlString(string.Format("<a href='{0}'><img class='icon {2}' src='{3}'/> {1}</a>", url, linkText,
                    @class, UrlUtility.ResolveUrl("~/Images/invis.gif")));
            }

            return new HtmlString("");
        }
        protected virtual string Class
        {
            get { return ""; }
        }
    }
}