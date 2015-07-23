using System;
using System.Collections.Generic;

namespace Bsc.Dmtds.Sites.Parsers.ThemeRule
{
    public interface IThemeRuleParser
    {
        IEnumerable<string> Parse(string cssHackBody, Func<string, string> replaceUrl, out string replacedCssHackBody);
 
    }
}