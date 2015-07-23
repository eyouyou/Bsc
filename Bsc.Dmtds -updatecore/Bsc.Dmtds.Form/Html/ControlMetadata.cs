using System.Collections.Generic;

namespace Bsc.Dmtds.Form.Html
{
    public class ControlMetadata
    {
        public string Name { get; set; }

        public string DataType { get; set; } 
    }
    public class ControlMetadataComparer : IEqualityComparer<ControlMetadata>
    {
        public bool Equals(ControlMetadata x, ControlMetadata y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(ControlMetadata obj)
        {
            return obj.GetHashCode();
        }
    }
}