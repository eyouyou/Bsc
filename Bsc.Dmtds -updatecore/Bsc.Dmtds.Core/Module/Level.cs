using System.ComponentModel.DataAnnotations;

namespace Bsc.Dmtds.Core.Module
{
    public class Level
    {
        [Key]
        public int LevelId { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        
    }
}