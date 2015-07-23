using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bsc.Dmtds.Core.Module.Account;

namespace Bsc.Dmtds.Core.Module
{
    public class BackModule
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
//        [ForeignKey("RoleId")]
//        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}