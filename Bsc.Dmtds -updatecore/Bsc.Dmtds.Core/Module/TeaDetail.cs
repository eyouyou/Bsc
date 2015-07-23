using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bsc.Dmtds.Core.Module.Account;

namespace Bsc.Dmtds.Core.Module
{
    public class TeaDetail
    {
        [Key]
        public string TeaId { get; set; }
        [ForeignKey("TeaId")]
        public virtual User User { get; set; }

        [MaxLength(300)]
        public string Brief { get; set; }
        
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
    }
}