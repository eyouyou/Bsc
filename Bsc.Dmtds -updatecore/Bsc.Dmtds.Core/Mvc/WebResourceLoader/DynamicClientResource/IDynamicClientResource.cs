using System.Collections.Generic;
using System.Security.AccessControl;

namespace Bsc.Dmtds.Core.Mvc.WebResourceLoader.DynamicClientResource
{
    public interface IDynamicClientResource
    {
        ResourceType ResourceType { get; }
        IEnumerable<string> SupportedFileExtensions { get; }
        bool Accept(string fileName);
        string RegisterResource(string filePath);
        /// <summary>
        /// Registers the client parser to parse the resource at client side.
        /// </summary>
        /// <returns></returns>
        string RegisterClientParser();
        /// <summary>
        /// Parses the client resource at the server side.
        /// </summary>
        /// <param name="virtualPath">Name of the file.</param>
        /// <returns></returns>
        string Parse(string virtualPath);
    }
}