namespace Bsc.Dmtds.Sites.Models
{
    public class ThemeRuleFile : ThemeFile
    {
        public ThemeRuleFile(string physicalPath)
            : base(physicalPath)
        {
        }
        public ThemeRuleFile(Theme theme)
            : base(theme, "Theme.rule")
        {
        }

    }
}