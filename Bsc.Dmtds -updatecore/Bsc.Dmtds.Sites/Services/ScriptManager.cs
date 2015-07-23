using System.Collections.Generic;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Services
{
    public class ScriptBaseDirectory : DirectoryResource
    {
        public ScriptBaseDirectory(Site site)
            : base(site, ScriptFile.PATH_NAME)
        {

        }
        protected ScriptBaseDirectory()
            : base()
        { }
        protected ScriptBaseDirectory(string physicalPath)
            : base(physicalPath)
        {
        }
        protected ScriptBaseDirectory(Site site, string name)
            : base(site, name)
        {
        }
        public override IEnumerable<string> RelativePaths
        {
            get { return new string[0]; }
        }

        public override IEnumerable<string> ParseObject(IEnumerable<string> relativePaths)
        {
            return relativePaths;
        }
    }
    public class ScriptManager : FileManager
    {
        protected override Models.DirectoryResource GetRootDir(Site site)
        {
            return new ScriptBaseDirectory(site);
        }

        public override string Type
        {
            get { return "Scripts"; }
        }
    }
}