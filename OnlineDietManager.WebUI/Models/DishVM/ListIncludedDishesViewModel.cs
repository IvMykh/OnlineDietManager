using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.DishesManagement;

namespace OnlineDietManager.WebUI.Models
{
    public class ListIncludedDishesViewModel
    {
        public IEnumerable<Dish> IncludedDishes { get; set; }
        public int               MealRefId { get; set; }
        public string            ReturnUrl { get; set; }

        public OwnerPolicy       OwnerPolicy { get; set; }
    }
}