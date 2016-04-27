using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineDietManager.Domain.DishesManagement
{
    [Table("Ingredients")]
    public class Ingredient
        : FoodComponent
    {
        [Key, Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please, specify the protein quantity")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive protein quantity")]
        public float Protein { get; set; }

        [Required(ErrorMessage = "Please, specify the fat quantity")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive fat quantity")]
        public float Fat { get; set; }

        [Required(ErrorMessage = "Please, specify the carbohydrates quantity")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive carbohydrates quantity")]
        public float Carbohydrates { get; set; }

        [Required(ErrorMessage = "Please, specify the caloricity")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive caloricity")]
        public float Caloricity { get; set; }
    }
}
