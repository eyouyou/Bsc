using System;
using System.Web;

namespace Bsc.Dmtds.Sites.Web
{
    public class BSCResponseModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("X-BSC-Version", Bsc.Dmtds.Sites.BSCVersion.Version);
        }
    }
}