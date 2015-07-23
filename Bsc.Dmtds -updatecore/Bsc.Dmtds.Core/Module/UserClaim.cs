using System.ComponentModel.DataAnnotations.Schema;

namespace Bsc.Dmtds.Core.Module
{
    public class UserClaim
    {
        [Column("UserClaimId")]
        public int Id { get; set; }
    }
}