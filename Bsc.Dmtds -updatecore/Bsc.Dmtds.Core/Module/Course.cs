using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bsc.Dmtds.Core.Module.Account;

namespace Bsc.Dmtds.Core.Module
{
    public class Course
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.ImageUrl)]
        public string ImgUrl { get; set; }
        [MaxLength(300)]
        public string Brief { get; set; }

        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }
        public ICollection<CourseVideo> CourseVideos { get; set; }
        public ICollection<CourseDoc> CourseDocs { get; set; }
    }
}