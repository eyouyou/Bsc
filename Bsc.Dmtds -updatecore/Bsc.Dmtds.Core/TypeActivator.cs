using System;

namespace Bsc.Dmtds.Core
{
    public class TypeActivator
    {
        public static Func<Type, object> CreateInstanceMethod = (type) =>
        {
            return Activator.CreateInstance(type);
        };
        public static object CreateInstance(Type type)
        {
            return CreateInstanceMethod(type);
        } 
    }
}