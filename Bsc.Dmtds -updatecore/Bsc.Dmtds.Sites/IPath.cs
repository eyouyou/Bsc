using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Runtime;

namespace Bsc.Dmtds.Sites
{
    public interface IPath
    {
        string PhysicalPath { get; }
        string VirtualPath { get; } 
    }
    public class CommonPath : IPath
    {
        public string PhysicalPath
        {
            get;
            set;
        }

        public string VirtualPath
        {
            get;
            set;
        }
    }

    public static class PathEx
    {
        static PathEx()
        {
            var baseDir = EngineContext.Current.Resolve<IBaseDir>();
            BasePath = baseDir.DataPathName;
        }
        public static string BasePath;
    }
}