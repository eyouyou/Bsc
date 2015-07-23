using System.Runtime.Serialization;

namespace Bsc.Dmtds.Content.Models
{
    [DataContract]
    public class OrderSetting
    {
        public static readonly OrderSetting Default = new OrderSetting() { FieldName = "UtcCreationDate", Direction = OrderDirection.Descending };
        [DataMember(Order = 1)]
        public string FieldName { get; set; }
        [DataMember(Order = 2)]
        public OrderDirection Direction { get; set; } 
    }
}