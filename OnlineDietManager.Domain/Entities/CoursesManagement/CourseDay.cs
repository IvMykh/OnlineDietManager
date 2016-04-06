using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.Entities.DishesManagement;

namespace OnlineDietManager.Domain.Entities.CoursesManagement
{
    [Table("CourseDays")]
    public class CourseDay
    {
        [Key, Column(Order = 0)]
        [ForeignKey("CourseRefID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Course Course { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("DayRefID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Day Day { get; set; }
    }
}
