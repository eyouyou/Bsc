using System;

namespace Bsc.Dmtds.Core.Mvc.Grid.Design
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class GridColumnAttribute : Attribute
    {
        public string HeaderText { get; set; }
        /// <summary>
        /// 实现IGridColumn接口
        /// 用于自定义输出列头
        /// </summary>
        /// <value>
        /// The type of the grid column.
        /// </value>
        public Type GridColumnType { get; set; }

        /// <summary>
        /// 实现IGridItemColumn接口
        /// 用于自定义输出行的列
        /// </summary>
        /// <value>
        /// The type of the grid item column.
        /// </value>
        public Type GridItemColumnType { get; set; }

        public int Order { get; set; }

        public override object TypeId
        {
            get
            {
                return this;
            }
        }
    }
}