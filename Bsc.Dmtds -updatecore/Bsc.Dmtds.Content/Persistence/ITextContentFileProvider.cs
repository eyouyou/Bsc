using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Content.Persistence
{
    public interface ITextContentFileProvider
    {
        string Save(TextContent content, ContentFile file);
        void DeleteFiles(TextContent content);
 
    }
}