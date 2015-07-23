using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Core.Module.Account
{
    [DataContract]
    public class Role : IIdentifiable
    {
        public Role()
        {
            Permissions = new List<Permission>();
        }

        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 3)]
        public List<Permission> Permissions { get; set; }

        public bool HasPermission(Permission permission)
        {
            return this.Permissions.Any(it => permission == it);
        }
        public string UUID
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }
    }
}