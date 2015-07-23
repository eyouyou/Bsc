﻿using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid;

namespace Bsc.Dmtds.Web.Grid
{
    public class BooleanGridItemColumn : GridItemColumn
    {
        public BooleanGridItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue) 
            : base(gridColumn, dataItem, propertyValue)
        {
        }
        public override IHtmlString RenderItemColumn(ViewContext viewContext)
        {
            var tip = "-";
            if (PropertyValue != null && ((bool)PropertyValue))
            {
                tip = "YES";
            }
            return new HtmlString(tip);
            //return new HtmlString(string.Format(@"<a href='javascript:;' title='{1}'><span class=""o-icon {0}""></span></a>", GetIconClass(PropertyValue), tip));
        }
    }
}