using System.Reflection;

namespace Bsc.Dmtds.Sites
{
    public class BSCVersion
    {
        static BSCVersion()
        {
            Assembly sitesAssembly = typeof(BSCVersion).Assembly;
            Version = sitesAssembly.GetName().Version.ToString();
        }
        public static string Version { get; private set; }

    }
}