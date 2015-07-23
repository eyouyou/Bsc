using System.Web;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea
{
    public static class ModuleUrlHelper
    {
        public static string Encode(string moduleUrl)
        {
            var encoded = moduleUrl.Replace("?", "~~");
            encoded = encoded.Replace("&amp;", "$$");
            encoded = encoded.Replace("&", "$$");
            return encoded;
        }
        public static string Decode(string encodedModuleUrl)
        {
            var url = HttpUtility.UrlDecode(encodedModuleUrl);
            var decoded = encodedModuleUrl.Replace("~~", "?");
            decoded = decoded.Replace("$$", "&");
            return decoded;
        }
        public static string RemoveApplicationPath(string moduleUrl, string applicationPath)
        {
            if (applicationPath != "/")
            {
                moduleUrl = moduleUrl.Remove(0, applicationPath.Length);
            }
            return moduleUrl;
        }
    }
}