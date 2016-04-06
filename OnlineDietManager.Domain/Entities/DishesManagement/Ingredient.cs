using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;

using OnlineDietManager.Domain.Entities.DishesManagement.Abstract;

namespace OnlineDietManager.Domain.Entities.DishesManagement
{
    [Table("Ingredients")]
    public class Ingredient
        : FoodComponent
    {
        [Key, Column(Order=0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public float Protein { get; set; }

        [Required]
        public float Fat { get; set; }

        [Required]
        public float Carbohydrates { get; set; }

        [Required]
        public float Caloricity { get; set; }
    }
}
