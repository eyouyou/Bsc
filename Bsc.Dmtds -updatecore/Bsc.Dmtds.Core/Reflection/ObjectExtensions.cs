namespace Bsc.Dmtds.Core.Reflection
{

    /// <summary>
    /// 
    /// </summary>
    public class Members
    {
        /// <summary>
        /// 初始化 <see cref="Members" /> 类.
        /// </summary>
        /// <param name="o">The o.</param>
        public Members(object o)
        {
            this.Properties = new Properties(o);
        }
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public Properties Properties { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Memberses the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static Members Members(this object o)
        {
            return new Members(o);
        }
    }
}