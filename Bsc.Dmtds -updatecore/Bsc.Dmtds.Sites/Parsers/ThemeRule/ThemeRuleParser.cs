using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.Services;

namespace Bsc.Dmtds.Sites.Parsers.ThemeRule
{
    public class ThemeRuleParser
    {
        public static IThemeRuleParser Parser = new RegularCssHackFileParser();
        public static IEnumerable<ThemeFile> Parse(Theme theme, out string themeRuleBody, string baseUri = null)
        {
            theme = theme.LastVersion();
            IEnumerable<ThemeFile> themeFiles = ServiceFactory.ThemeManager.AllStyles(theme);
            ThemeRuleFile cssHackFile = ServiceFactory.ThemeManager.GetCssHack(theme);
            if (cssHackFile == null || !cssHackFile.Exists())
            {
                themeRuleBody = "";
                return themeFiles;
            }

            var themeRuleFiles = Parser.Parse(cssHackFile.Read(), (fileVirtualPath) => UrlUtility.ToHttpAbsolute(baseUri, new ThemeFile(theme, fileVirtualPath).LastVersion().VirtualPath), out themeRuleBody);

            return themeFiles.Where(it => !themeRuleFiles.Any(cf => cf.EqualsOrNullEmpty(it.FileName, StringComparison.CurrentCultureIgnoreCase)));
        } 
    }
}