using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bsc.Dmtds.Sites.Models
{
    [DataContract]
    public class HtmlMeta
    {
        [DataMember(Order = 1)]
        public string Author { get; set; }
        [DataMember(Order = 3)]
        public string Keywords { get; set; }
        [DataMember(Order = 5)]
        public string Description { get; set; }

        [DataMember(Order = 7)]
        public Dictionary<string, string> Customs
        {
            get;
            set;
        }
        [DataMember(Order = 8)]
        public string HtmlTitle { get; set; }

        [DataMember(Order = 10)]
        public string Canonical { get; set; }
        [DataMember]
        /// To allow write custom html meta block. for example: <meta http-equiv="charset" content="XXX"> <meta property="title" content="{title}">
        public string HtmlMetaBlock { get; set; }
    }
}