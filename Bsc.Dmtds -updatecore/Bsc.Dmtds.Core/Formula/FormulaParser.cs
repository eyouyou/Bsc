using System.Text.RegularExpressions;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Core.Formula
{
    [Dependency(typeof(IFormulaParser))]
    public class FormulaParser : IFormulaParser
    {
        public virtual string Populate(string formula, IValueProvider valueProvider)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return null;
            }
            var matches = Regex.Matches(formula, "{(?<Name>[^{^}]+)}");
            var s = formula;
            foreach (Match match in matches)
            {
                var key = match.Groups["Name"].Value;
                int intKey;
                //ignore {0},{1}... it is the format string.
                if (!int.TryParse(key, out intKey))
                {
                    var value = valueProvider.GetValue(match.Groups["Name"].Value);
                    var htmlValue = value == null ? "" : value.ToString();
                    s = s.Replace(match.Value, htmlValue);
                }

            }
            return s;
        }
    }
}