﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bsc.Dmtds.Common;
namespace Bsc.Dmtds.Sites
{
    public static class IEnumerableStringExtension
    {
        public static IEnumerable<string> ExcludeSvn(this IEnumerable<string> dirs)
        {
            return dirs.Where(it => !(it.Contains(".svn", StringComparison.OrdinalIgnoreCase) || it.Contains("_svn", StringComparison.OrdinalIgnoreCase)));
        } 
    }
}