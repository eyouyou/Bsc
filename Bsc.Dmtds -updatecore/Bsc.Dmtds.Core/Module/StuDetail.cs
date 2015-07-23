using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bsc.Dmtds.Core.Module.Account;

namespace Bsc.Dmtds.Core.Module
{
    public class StuDetail
    {
        [Key]
        public string StuId { get; set; }
        [ForeignKey("StuId")]
        public User User { get; set; }
        
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }
        public int Grade { get; set; }
    }

}