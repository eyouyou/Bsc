using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Sites.Models;

namespace Bsc.Dmtds.Sites.Services
{
    [Dependency(typeof(CodeSnippetManager))]
    public class CodeSnippetManager
    {
        protected string BasePath
        {
            get
            {
                return Bsc.Dmtds.Core.Mvc.AreaHelpers.CombineAreaFilePhysicalPath("Sites", "CodeSnippets");
            }
        }
        protected virtual string FileExtension
        {
            get
            {
                return ".txt";
            }
        }

        public virtual CodeSnippetGroup All(string viewEngine)
        {
            var physicalPath = Path.Combine(BasePath, viewEngine);
            CodeSnippetGroup root = new CodeSnippetGroup() { Name = "root", Parent = null, ViewEngine = viewEngine };
            if (Directory.Exists(physicalPath))
            {
                root.ChildGroups = AllGroups(viewEngine, root, physicalPath);
                root.CodeSnippets = AllCodeSnippet(root, physicalPath);
            }
            return root;
        }
        private IEnumerable<CodeSnippetGroup> AllGroups(string viewEngine, CodeSnippetGroup parent, string physicalPath)
        {
            var dir = Directory.GetDirectories(physicalPath);
            if (dir.Length != 0)
            {
                foreach (var item in dir.ExcludeSvn())
                {
                    var group = new CodeSnippetGroup()
                    {
                        ViewEngine = viewEngine,
                        Name = Path.GetFileNameWithoutExtension(item),
                        Parent = parent
                    };
                    group.ChildGroups = AllGroups(viewEngine, parent, item);
                    group.CodeSnippets = AllCodeSnippet(group, item);
                    yield return group;
                }
            }
        }

        private IEnumerable<CodeSnippet> AllCodeSnippet(CodeSnippetGroup group, string physicalPath)
        {
            return Directory.EnumerateFiles(physicalPath, "*" + FileExtension).Select(it =>
                new CodeSnippet()
                {
                    Group = group,
                    ViewEngine = group.ViewEngine,
                    Name = Path.GetFileNameWithoutExtension(it),
                    Code = IOUtility.ReadAsString(it)
                });
        }
    }
}