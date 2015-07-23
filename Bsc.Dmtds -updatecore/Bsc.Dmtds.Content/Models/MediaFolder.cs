using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bsc.Dmtds.Content.Models
{
    [DataContract(Name = "MediaFolder")]
    [KnownTypeAttribute(typeof(MediaFolder))]
    public class MediaFolder : Folder
    {
        public MediaFolder() { }
        public MediaFolder(Repository repository, string fullName) : base(repository, fullName) { }
        public MediaFolder(Repository repository, string name, Folder parent)
            : base(repository, name, parent)
        { }
        public MediaFolder(Repository repository, IEnumerable<string> namePath) : base(repository, namePath) { }

        [DataMember(Order = 5)]
        public string[] AllowedExtensions { get; set; }

    }
}