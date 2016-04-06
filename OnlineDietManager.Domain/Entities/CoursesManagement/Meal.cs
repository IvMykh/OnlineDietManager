using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using OnlineDietManager.Domain.Entities.Abstract;
using OnlineDietManager.Domain.Entities.DishesManagement;

namespace OnlineDietManager.Domain.Entities.CoursesManagement
{
    [Table("Meals")]
    public class Meal
        : IDescribable
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID          { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public virtual IList<Dish> Dishes { get; set; }
    }
}
