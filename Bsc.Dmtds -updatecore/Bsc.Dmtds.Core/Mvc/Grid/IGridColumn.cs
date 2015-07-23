﻿using System.Web;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid.Design;

namespace Bsc.Dmtds.Core.Mvc.Grid
{
    public interface IGridColumn
    {
        /// <summary>
        /// Gets the grid model.
        /// </summary>
        IGridModel GridModel { get; }

        /// <summary>
        /// Gets the column attribute.
        /// </summary>
        GridColumnAttribute ColumnAttribute { get; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        string PropertyName { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 输出列头
        /// </summary>
        /// <returns></returns>
        IHtmlString RenderHeader(ViewContext viewContext);

        /// <summary>
        /// 输出th（容器）的HTML属性
        /// </summary>
        /// <returns></returns>
        IHtmlString RenderHeaderContainerAtts(ViewContext viewContext); 
    }
    public class GridColumn : IGridColumn
    {
        public GridColumn(GridModel gridModel, GridColumnAttribute att, string propertyName, int order)
        {
            this.GridModel = gridModel;
            this.ColumnAttribute = att;
            this.PropertyName = propertyName;
            this.Order = order;
        }
        public virtual IHtmlString RenderHeader(ViewContext viewContext)
        {
            return new HtmlString((string.IsNullOrEmpty(ColumnAttribute.HeaderText) ? PropertyName : ColumnAttribute.HeaderText));
        }

        public virtual IHtmlString RenderHeaderContainerAtts(ViewContext viewContext)
        {
            return new HtmlString("");
        }

        public virtual string PropertyName
        {
            get;
            private set;
        }

        public virtual GridColumnAttribute ColumnAttribute
        {
            get;
            private set;
        }


        public virtual int Order
        {
            get;
            private set;
        }

        public virtual IGridModel GridModel
        {
            get;
            private set;
        }
    }
}