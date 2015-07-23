using System.Collections.Generic;
using Bsc.Dmtds.Core.Mvc.Paging;

namespace Bsc.Dmtds.Sites.DataRule
{
    public class DataRulePagedList : PagedList<IDictionary<string, object>>
    {
        public DataRulePagedList(IEnumerable<IDictionary<string, object>> items, int pageIndex, int pageSize, int totalItemCount)
            : base(items, pageIndex, pageSize, totalItemCount)
        {
            PageIndexParameterName = "PageIndex";
        }
        public string PageIndexParameterName { get; set; }
    }
}