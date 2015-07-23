using System;

namespace Bsc.Dmtds.Sites.Services
{
    public class FriendlyException : Exception
    {
        public FriendlyException(string msg) : base(msg) { }
        public FriendlyException(string msg, Exception inner) : base(msg, inner) { }

    }
}