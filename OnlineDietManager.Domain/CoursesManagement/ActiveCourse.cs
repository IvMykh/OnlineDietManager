using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.CoursesManagement
{
    [Table("ActiveCourses")]
    public class ActiveCourse
        //: Course
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        //[Key, Column(Order = 1)]
        public DateTime StartDate { get; set; }

        [ForeignKey("ID")]
        public virtual Course Course { get; set; }
    }
}
