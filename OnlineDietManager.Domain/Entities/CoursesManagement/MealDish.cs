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
    [Table("MealDishes")]
    public class MealDish
    {
        [ForeignKey("MealID")]
        public Meal Meal { get; set; }

        [ForeignKey("DishID")]
        public virtual Dish Dish { get; set; }

        [Key, Column(Order = 0)]
        public int MealID { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DishID { get; set; }
    }
}
