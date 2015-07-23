using Bsc.Dmtds.Core.Mvc;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Content.Services
{
    [Dependency(typeof(SchemaTemplateManager))]
    public class SchemaTemplateManager : ItemTemplateManager
    {
        protected override string TemplatePath
        {
            get
            {
                return AreaHelpers.CombineAreaFilePhysicalPath("Contents", "Templates", "Schema");
            }
        }
    }
}