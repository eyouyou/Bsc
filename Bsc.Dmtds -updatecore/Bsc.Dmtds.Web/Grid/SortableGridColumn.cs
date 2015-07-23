using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;
using Bsc.Dmtds.Core.Mvc.Grid.Design;

namespace Bsc.Dmtds.Web.Grid
{
    public class SortableGridColumn : GridColumn
    {
        public SortableGridColumn(GridModel gridModel, GridColumnAttribute att, string propertyName, int order) 
            : base(gridModel, att, propertyName, order)
        {
        }
        public override IHtmlString RenderHeaderContainerAtts(ViewContext viewContext)
        {
            return new HtmlString("class='" + SortByExtension.RenderSortHeaderClass(viewContext.RequestContext, this.PropertyName, this.Order).ToString() + "'");
        }

        public override IHtmlString RenderHeader(ViewContext viewContext)
        {
            return SortByExtension.RenderGridHeader(viewContext.RequestContext, base.RenderHeader(viewContext).ToString(), this.PropertyName, this.Order);
        }
    }
}