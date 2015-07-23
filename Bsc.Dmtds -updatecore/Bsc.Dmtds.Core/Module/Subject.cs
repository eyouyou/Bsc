using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bsc.Dmtds.Core.Module
{
    public class Subject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }

        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }


    }
}