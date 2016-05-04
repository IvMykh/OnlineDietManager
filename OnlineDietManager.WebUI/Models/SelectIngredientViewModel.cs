using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.DishesManagement;

namespace OnlineDietManager.WebUI.Models
{
    public class SelectIngredientViewModel
    {
        public Ingredient Ingredient { get; set; }
        public float Weight { get; set; }
        public int DishRefId { get; set; }
        public string ReturnUrl { get; set; }
    }
}