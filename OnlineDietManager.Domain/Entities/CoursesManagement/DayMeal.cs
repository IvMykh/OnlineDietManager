using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.CoursesManagement
{
    [Table("DayMeals")]
    public class CourseDay
    {
        [Key, Column(Order = 0)]
        [ForeignKey("DayRefID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Day Day { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("MealRefID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Meal Meal { get; set; }
    }
}
