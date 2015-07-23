using System;
using System.Runtime.Serialization;

namespace Bsc.Dmtds.Dto
{
    [DataContract]
    public class UserDto
    {
        public UserDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [DataMember]
        public string ImgUrl { get; set; }
        [DataMember]
        public string Id { get; private set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        [DataMember]
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
    }
}