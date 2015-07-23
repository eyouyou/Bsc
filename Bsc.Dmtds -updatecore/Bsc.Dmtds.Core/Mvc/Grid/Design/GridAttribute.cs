using System;

namespace Bsc.Dmtds.Core.Mvc.Grid.Design
{
    public class GridAttribute:Attribute
    {
        public bool Checkable { get; set; }
        public bool Draggable { get; set; }
        public string IdProperty { get; set; }
        /// <summary>
        /// The custom grid model type inherit from IGridModel.
        /// </summary>
        public Type GridModelType { get; set; }

        /// <summary>
        /// The custom grid item type inherit from IGridItem
        /// </summary>
        /// <value>
        /// The type of the grid item.
        /// </value>
        public Type GridItemType { get; set; }

        public string EmptyText { get; set; }
    }
}