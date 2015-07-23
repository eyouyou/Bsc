using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.View.CodeSnippet
{
    public interface IDataRuleCodeSnippet
    {
        string Generate(Repository repository, DataRuleSetting dataRule, bool inlineEdit);
 
    }
}