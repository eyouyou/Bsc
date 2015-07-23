using System.Runtime.Serialization;
using Bsc.Dmtds.Sites.DataRule;

namespace Bsc.Dmtds.Sites.Models
{
    public enum TakeOperation
    {
        List,
        First,
        Count
    }
    [DataContract]
    public class DataRuleSetting
    {
        [DataMember(Order = 1)]
        public string DataName { get; set; }
        [DataMember(Order = 3)]
        public IDataRule DataRule { get; set; }
        [DataMember(Order = 5)]
        public TakeOperation TakeOperation { get; set; }

        /// <summary>
        /// The time, in seconds, that the data rule is cached. 
        /// </summary>
        [DataMember(Order = 7)]
        public int CachingDuration { get; set; }

    }
}