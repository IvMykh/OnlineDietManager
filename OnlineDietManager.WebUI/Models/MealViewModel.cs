using OnlineDietManager.Domain.CoursesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class MealViewModel
    {
        public Meal Meal { get; set; }
        public string ReturnUrl { get; set; }
        public int DayId { get; set; }
        public IEnumerable<Domain.DishesManagement.Dish> AllDishes { get; set; }
        public int SelectedDishId { get; set; }
    }
}