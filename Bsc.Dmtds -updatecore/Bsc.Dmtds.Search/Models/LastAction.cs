using System;
using System.Runtime.Serialization;
using Bsc.Dmtds.Content.Models;

namespace Bsc.Dmtds.Search.Models
{
    public class LastAction
    {
        public Repository Repository { get; set; }
        [DataMember(Order = 1)]
        public string FolderName { get; set; }
        [DataMember(Order = 2)]
        public string ContentSummary { get; set; }
        [DataMember(Order = 3)]
        public ContentAction Action { get; set; }
        [DataMember(Order = 4)]
        public DateTime ActionDate { get; set; } 
    }
}