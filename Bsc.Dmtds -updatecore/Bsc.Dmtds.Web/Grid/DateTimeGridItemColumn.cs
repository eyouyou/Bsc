using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;

namespace Bsc.Dmtds.Web.Grid
{
    public class DateTimeGridItemColumn : GridItemColumn
    {
        public DateTimeGridItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue) : base(gridColumn, dataItem, propertyValue)
        {
        }
        public override IHtmlString RenderItemColumn(ViewContext viewContext)
        {
            string s = "-";
            if (PropertyValue != null)
            {
                var dateTime = ((DateTime)PropertyValue);
                if (dateTime == DateTime.MinValue)
                {
                    s = "-";
                }
                else
                {
                    s = dateTime.ToString(CultureInfo.CurrentCulture);
                }
            }
            return new HtmlString(s);
        }
    }
}