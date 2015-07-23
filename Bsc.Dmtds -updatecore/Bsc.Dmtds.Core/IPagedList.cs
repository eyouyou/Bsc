using System.Collections.Generic;
using Bsc.Dmtds.Core.Mvc.Paging;

namespace Bsc.Dmtds.Core
{
    public interface IPagedList<T>: IEnumerable<T>, IPagedList
    {
        /// <summary>
        /// 当前页面index
        /// </summary>
        /// <value>
        /// 当前页面索引
        /// </value>
        int CurrentPageIndex { get; set; }
        /// <summary>
        /// set/get 页面大小
        /// </summary>
        /// <value>
        /// 页面大小
        /// </value>
        int PageSize { get; set; }
        /// <summary>
        /// set/get 总数量
        /// </summary>
        int TotalItemCount { get; set; }
        /// <summary>
        /// 得到总页数
        /// </summary>
        /// <value>
        /// 总页数
        /// </value>
        int TotalPageCount { get; }
        /// <summary>
        /// 得到开始记录的索引
        /// </summary>
        /// <value>
        /// 开始记录的索引
        /// </value>
        int StartRecordIndex { get; }
        /// <summary>
        /// 得到结束索引
        /// </summary>
        /// <value>
        /// 结束索引
        /// </value>
        int EndRecordIndex { get; }
    }
}