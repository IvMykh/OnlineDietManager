using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class CreateDishComponentViewModel
    {
        [Required(ErrorMessage = "Please, specify corresponding Ingredient")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, specify corresponding Dish")]
        public int DishRefId { get; set; }

        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "Please, specify corresponding weight of component")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Component weight cannot be negative")]
        public float Weight { get; set; }
    }
}