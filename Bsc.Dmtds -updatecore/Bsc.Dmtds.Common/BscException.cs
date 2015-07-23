using System;

namespace Bsc.Dmtds.Common
{
    public class BscException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KoobooException" /> class.
        /// </summary>
        public BscException()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="KoobooException" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public BscException(string msg)
            : base(msg)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="KoobooException" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="exception">The exception.</param>
        public BscException(string msg, Exception exception)
            : base(msg, exception)
        { }
    }
}