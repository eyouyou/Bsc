using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;
using Bsc.Dmtds.Core.Mvc.Grid.Design;

namespace Bsc.Dmtds.Web.Grid
{
    public class ActionGridColumn:GridColumn
    {
        public ActionGridColumn(GridModel gridModel, GridColumnAttribute att, string propertyName, int order) 
            : base(gridModel, att, propertyName, order)
        {
        }
        public override IHtmlString RenderHeaderContainerAtts(ViewContext viewContext)
        {
            return new HtmlString("class='action'");
        }
    }
}