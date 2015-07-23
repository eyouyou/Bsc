using System;

namespace Bsc.Dmtds.Sites.View
{
    [Flags]
    public enum EditingType
    {
        Label = 1,
        Content = 2,
        Page = 4
         
    }
}