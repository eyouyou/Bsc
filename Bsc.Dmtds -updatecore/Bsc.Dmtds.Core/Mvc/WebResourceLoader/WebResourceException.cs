using System;

namespace Bsc.Dmtds.Core.Mvc.WebResourceLoader
{
    public class WebResourceException : Exception
    {
        public WebResourceException(string msg)
            : base(msg)
        {
        }   
    }
}