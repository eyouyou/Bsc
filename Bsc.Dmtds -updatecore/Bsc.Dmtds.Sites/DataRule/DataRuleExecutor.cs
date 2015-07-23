using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.DataRule
{
    public static class DataRuleExecutor
    {
        public static string ModelName = "model";
        public static void Execute(ViewDataDictionary viewData, DataRuleContext dataRuleContext, IEnumerable<DataRuleSetting> dataRules)
        {
            foreach (var item in dataRules)
            {
                var data = item.DataRule.Execute(dataRuleContext, item.TakeOperation, item.CachingDuration);
                if (item.DataName.EqualsOrNullEmpty(ModelName, StringComparison.CurrentCultureIgnoreCase))
                {
                    viewData.Model = data;
                }
                viewData[item.DataName] = data;
            }
        } 
    }
}