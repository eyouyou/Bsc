using System.Web;
using System.Web.Mvc;

namespace Bsc.Dmtds.Core.Mvc.Grid
{
    public interface IGridItemColumn
    {
        IGridColumn GridColumn { get; }

        /// <summary>
        /// 行数据对象
        /// </summary>
        object DataItem { get; }

        /// <summary>
        /// 属性值
        /// </summary>
        object PropertyValue { get; }

        /// <summary>
        /// 输出td的HTML属性
        /// </summary>
        /// <returns></returns>
        IHtmlString RenderItemColumnContainerAtts(ViewContext viewContext);
        /// <summary>
        /// 输出td的HTML文本
        /// </summary>
        /// <returns></returns>
        IHtmlString RenderItemColumn(ViewContext viewContext);   
    }

    public class GridItemColumn : IGridItemColumn
    {
        public GridItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue)
        {
            this.GridColumn = gridColumn;
            this.DataItem = dataItem;
            this.PropertyValue = propertyValue;
        }

        public virtual IGridColumn GridColumn
        {
            get;
            private set;
        }

        public virtual object DataItem
        {
            get;
            private set;
        }

        public virtual object PropertyValue
        {
            get;
            private set;
        }

        public virtual IHtmlString RenderItemColumnContainerAtts(ViewContext viewContext)
        {
            return new HtmlString("");
        }

        public virtual IHtmlString RenderItemColumn(ViewContext viewContext)
        {
            return new HtmlString((PropertyValue == null || PropertyValue.ToString() == "") ? "-" : HttpUtility.HtmlEncode(PropertyValue.ToString()));
        }
    }
}