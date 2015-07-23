using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.DataRule
{
    public interface IDataRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRuleContext"></param>
        /// <param name="operation">The type of the query, List all data, get the first item or query the count.</param>
        /// <param name="cachingDuration">The cache time, in second.</param>
        /// <returns></returns>
        object Execute(DataRuleContext dataRuleContext, TakeOperation operation, int cacheDuration);
        DataRuleType DataRuleType { get; }

        bool HasAnyParameters(); 
    }
}