using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Bsc.Dmtds.Core.Mvc.Grid.Design;

namespace Bsc.Dmtds.Core.Mvc.Grid
{
    public interface IGridModel
    {
        /// <summary>
        /// Gets the grid attribute.
        /// </summary>
        GridAttribute GridAttribute { get; }

        /// <summary>
        /// Gets the model metadata.
        /// </summary>
        ModelMetadata ModelMetadata { get; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        IEnumerable DataSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IGridModel"/> is checkable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checkable; otherwise, <c>false</c>.
        /// </value>
        bool Checkable { get; set; }

        /// <summary>
        /// Gets or sets the id porperty.
        /// </summary>
        string IdPorperty { get; set; }

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        ViewContext ViewContext { get; set; }

        IEnumerable<IGridColumn> Columns { get; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGridItem> GetItems(); 
    }
}