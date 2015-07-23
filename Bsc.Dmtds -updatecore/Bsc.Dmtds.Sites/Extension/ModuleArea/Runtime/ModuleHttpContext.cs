using System.Web;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public class ModuleHttpContext : HttpContextWrapper
    {
        HttpRequestBase _httpRequest;
        HttpResponseBase _httpResponse;

        public ModuleHttpContext(HttpContext httpContext, HttpRequestBase httpRequest, HttpResponseBase httpResponse, ModuleContext moduleContext)
            : base(httpContext)
        {
            this._httpRequest = httpRequest;
            this._httpResponse = httpResponse;
            this.ModuleContext = moduleContext;
        }

        public ModuleContext ModuleContext { get; private set; }


        public override HttpRequestBase Request
        {
            get
            {
                return this._httpRequest;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return this._httpResponse;
            }
        }

    }
}