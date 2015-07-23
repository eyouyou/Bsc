using System;
using System.Runtime.Serialization;

namespace Bsc.Dmtds.Dto
{
    [DataContract]
    public class RoleDto
    {
        public RoleDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }      
    }
}