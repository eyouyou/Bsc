using System;

namespace Bsc.Dmtds.Core.Runtime.Dependency
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependencyAttribute:Attribute
    {
        public DependencyAttribute(ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            LifeStyle = lifeStyle;
        }

        public DependencyAttribute(Type serviceType, ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            LifeStyle = lifeStyle;
            ServiceType = serviceType;
        }

        /// <summary>The type of service the attributed class represents.</summary>
        public Type ServiceType { get; protected set; }

        public ComponentLifeStyle LifeStyle { get; protected set; }

        /// <summary>Optional key to associate with the service.</summary>
        public string Key { get; set; }

        /// <summary>Configurations for which this service is registered.</summary>
        public string Configuration { get; set; }

        /// <summary>
        /// The order to add the component into IoC container.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }
    }
}