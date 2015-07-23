using System.IO;
using Bsc.Dmtds.Content.Models.Paths;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.Models
{
    public static class ContentExtensions
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static Repository GetRepository(this ContentBase content)
        {
            return new Repository(content.Repository);
        }
        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TextFolder GetFolder(this TextContent content)
        {
            return new TextFolder(content.GetRepository(), FolderHelper.SplitFullName(content.FolderName));
        }
        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static MediaFolder GetFolder(this MediaContent content)
        {
            return new MediaFolder(content.GetRepository(), FolderHelper.SplitFullName(content.FolderName));
        }
        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <param name="textContent">Content of the text.</param>
        /// <returns></returns>
        public static Schema GetSchema(this TextContent textContent)
        {
            return new Schema(textContent.GetRepository(), textContent.SchemaName);
        }
        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static string GetSummary(this ContentBase content)
        {
            if (content is TextContent)
            {
                var textContent = (TextContent)content;
                var schema = textContent.GetSchema().AsActual();
                var summarizeField = schema.GetSummarizeColumn();
                if (summarizeField != null && content.ContainsKey(summarizeField.Name))
                {
                    return content[summarizeField.Name] == null ? "" : content[summarizeField.Name].ToString();
                }
                return "";
            }
            else
            {
                var mediaContent = (MediaContent)content;
                return mediaContent.FileName;
            }
        }
        /// <summary>
        /// Exists the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static bool Exist(this MediaContent content)
        {
            var contentPath = new MediaContentPath(content);
            return File.Exists(contentPath.PhysicalPath);
        }
    }
}