using System.IO;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Models.Paths;

namespace Bsc.Dmtds.Search
{
    public class SearchDir
    {
        public static string GetBasePhysicalPath(Repository repository)
        {
            var repositoryPath = new RepositoryPath(repository);
            return Path.Combine(repositoryPath.PhysicalPath, SearchDir.DirName);
        }
        public static string DirName = "Search"; 
    }
}