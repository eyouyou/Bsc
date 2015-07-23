using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Bsc.Dmtds.Core.Module.Account;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Core.Module
{
    [DataContract]
    public class UserRole:IIdentifiable
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
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
        public List<Permission> Permissions { get; set; }

        public bool HasPermission(Permission permission)
        {
            return this.Permissions.Any(it => permission == it);
        }
    }
}