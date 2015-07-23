using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bsc.Dmtds.Core.Module
{
    public class CourseDoc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public DateTime UpDate { get; set; }
//        [ForeignKey("UserId")]
//        public String BelongTo { get; set; }
//        public virtual User User { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.ImageUrl)]
        public string Cover { get; set; }
    }
}