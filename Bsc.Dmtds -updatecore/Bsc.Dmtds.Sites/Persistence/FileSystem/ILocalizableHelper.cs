using System.IO;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Persistence.FileSystem
{
    public static class ILocalizableHelper
    {
        public static void Localize<T>(T source, Site targetSite) where T : PathResource, IInheritable<T>, new()
        {
            if (source.Site != targetSite)
            {
                var target = new T();
                ((PathResource)target).Site = targetSite;
                target.Name = source.Name;

                CopyFiles(source.PhysicalPath, target.PhysicalPath);
            }

        }

        public static void CopyFiles(string varFromDirectory, string varToDirectory)
        {
            if (Directory.Exists(varFromDirectory))
            {
                IOUtility.CopyDirectory(varFromDirectory, varToDirectory, false);
            }
        }
    }
}