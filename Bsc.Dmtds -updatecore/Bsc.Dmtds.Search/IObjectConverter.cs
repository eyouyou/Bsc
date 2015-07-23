using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Bsc.Dmtds.Core.Runtime;
using Bsc.Dmtds.Search.Models;

namespace Bsc.Dmtds.Search
{
    public interface IObjectConverter
    {
        KeyValuePair<string, string> GetKeyField(object o);

        IndexObject GetIndexObject(object o);

        object GetNativeObject(NameValueCollection fields);

        string GetUrl(object nativeObject); 
    }
    public class ObjectConverters
    {
        public static IObjectConverter GetConverter(Type type)
        {
            return EngineContext.Current.TryResolve<IObjectConverter>(type.FullName);
        }
    }
}