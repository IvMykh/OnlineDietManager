using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.Abstract;
using OnlineDietManager.Domain.DishesManagement;

namespace OnlineDietManager.Domain.CoursesManagement
{
    [Table("Meals")]
    public class Meal
        : IDescribable
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Description { get; set; }

        [Required]
        public TimeSpan Time { get; set; } //DateTime

        [Required]
        public virtual IList<Dish> Dishes { get; set; }
    }
}
