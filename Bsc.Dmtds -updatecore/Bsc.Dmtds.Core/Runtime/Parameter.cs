using System;

namespace Bsc.Dmtds.Core.Runtime
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            this.Name = name;
            this.ValueCallback = () => value;
        }
        public Parameter(string name, Func<object> value)
        {
            this.Name = name;
            this.ValueCallback = value;
        }
        public string Name { get; private set; }
        public Func<object> ValueCallback { get; private set; } 
    }
}