using System.Runtime.Serialization;

namespace Bsc.Dmtds.Sites.Models
{
    [DataContract]
    public class LayoutPosition
    {
        [DataMember(Order = 1)]
        public string ID { get; set; }

    }
}