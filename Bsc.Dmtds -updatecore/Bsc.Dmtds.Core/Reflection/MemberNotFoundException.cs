using System;

namespace Bsc.Dmtds.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberNotFoundException : Exception
    {
        #region 构造函数
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberNotFoundException" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public MemberNotFoundException(string msg)
            : base(msg)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberNotFoundException" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="inner">The inner.</param>
        public MemberNotFoundException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
        #endregion

        #region 属性
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; set; }
        #endregion
    }
}