using System;
using System.Collections.Generic;

namespace Bsc.Dmtds.Core.Linq
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 0;
            foreach (var item in source)
            {
                action(item, index);
                index++;
            }
        } 
    }
}