using System;

namespace Bsc.Dmtds.Core.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataSourceAttribute : Attribute
    {
        public DataSourceAttribute(Type dataSourceType)
        {
            this.DataSourceType = dataSourceType;
        }
        public Type DataSourceType { get; private set; }
    }
}