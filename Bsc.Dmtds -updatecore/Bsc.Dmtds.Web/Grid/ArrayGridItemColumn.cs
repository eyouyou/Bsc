using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;

namespace Bsc.Dmtds.Web.Grid
{
    public class ArrayGridItemColumn : GridItemColumn
    {
        public ArrayGridItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue) 
            : base(gridColumn, dataItem, propertyValue)
        {
        }
        public override IHtmlString RenderItemColumn(ViewContext viewContext)
        {
            if (PropertyValue != null)
            {
                return new HtmlString(string.Join(",", ((IEnumerable<string>)PropertyValue).ToArray()));
            }
            return new HtmlString("");
        }
    }
}