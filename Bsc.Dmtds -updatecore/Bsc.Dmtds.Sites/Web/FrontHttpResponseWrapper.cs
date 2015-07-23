using System.Web;

namespace Bsc.Dmtds.Sites.Web
{
    public class FrontHttpResponseWrapper : System.Web.HttpResponseWrapper
    {
        FrontHttpContextWrapper _context;
        public FrontHttpResponseWrapper(HttpResponse httpResponse, FrontHttpContextWrapper context)
            : base(httpResponse)
        {
            _context = context;
        }
        public override void End()
        {
            if (this.Output is OutputTextWriterWrapper)
            {
                ((OutputTextWriterWrapper)this.Output).Render(this);
            }
            base.End();
        }
    }
}