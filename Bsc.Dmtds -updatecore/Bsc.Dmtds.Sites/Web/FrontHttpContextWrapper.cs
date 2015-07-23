using System.Web;
using Bsc.Dmtds.Sites.Extension;

namespace Bsc.Dmtds.Sites.Web
{
    public class FrontHttpContextWrapper : HttpContextWrapper
    {
        private readonly HttpContext _context;

        public FrontHttpContextWrapper(HttpContext httpContext)
            : base(httpContext)
        {
            _context = httpContext;
        }
        FrontHttpRequestWrapper request;
        public override HttpRequestBase Request
        {
            get
            {
                if (request == null)
                {
                    request = new FrontHttpRequestWrapper(_context.Request);
                    PageRequestModuleExecutor.OnResolvingSite(this);
                    request.ResolveSite();
                    PageRequestModuleExecutor.OnResolvedSite(this);
                }
                return request;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return new FrontHttpResponseWrapper(_context.Response, this);
            }
        }

        public FrontHttpRequestWrapper RequestWrapper
        {
            get
            {
                return (FrontHttpRequestWrapper)this.Request;
            }
        }
        public FrontHttpResponseWrapper ResponseWrapper
        {
            get
            {
                return (FrontHttpResponseWrapper)this.Response;
            }
        }
    }
}