using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Services
{
    public class Size
    {
        public float Width { get; set; }
        public float Height { get; set; }
    }
    [Dependency(typeof(HeaderBackgroundManager))]
    public class HeaderBackgroundManager
    {
        public virtual bool IsEanbled(Theme theme)
        {
            return false;
        }
        public virtual void Change(Theme theme, string originalFile, int x, int y)
        {

        }
        public virtual string GetVirutalPath(Theme theme)
        {
            return "";
        }
        public virtual Size GetContainerSize(Bsc.Dmtds.Sites.Models.Site site)
        {
            return new Size();
        }
    }
}