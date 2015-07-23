using System.Collections.Specialized;

namespace Bsc.Dmtds.Sites.DataRule.Http
{
    public interface IHttpDataRequest
    {
        dynamic GetData(string url, string httpMethod, string contentType, NameValueCollection form, NameValueCollection headers);
 
    }
}