using System;

namespace Bsc.Dmtds.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberException" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public MemberException(string msg)
            : base(msg)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberException" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="inner">The inner.</param>
        public MemberException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }
}