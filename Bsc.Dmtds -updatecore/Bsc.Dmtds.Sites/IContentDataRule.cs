using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Sites.DataRule;

namespace Bsc.Dmtds.Sites
{
    public interface IContentDataRule : IDataRule
    {
        string FolderName { get; set; }

        Schema GetSchema(Repository repository);

        bool IsValid(Repository repository);

        bool? EnablePaging { get; set; }
    }
}