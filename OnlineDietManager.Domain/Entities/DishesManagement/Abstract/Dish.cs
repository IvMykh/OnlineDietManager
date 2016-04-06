using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;

using OnlineDietManager.Domain.Practice.Entities.DishesManagement.Abstract;

namespace OnlineDietManager.Domain.Practice.Entities.DishesManagement
{
    [Table("Dishes")]
    public class Dish
        : FoodComponent
    {
        [Key, Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public virtual IList<DishComponent> Components
        {
            get;
            private set;
        }

        public Dish()
        {
            Components = new List<DishComponent>();
        }

        public float Protein
        {
            get { return Components.Sum(comp => comp.Protein); }
        }

        public float Fat 
        {
            get { return Components.Sum(comp => comp.Fat); }
        }

        public float Carbohydrates 
        {
            get { return Components.Sum(comp => comp.Carbohydrates); }
        }

        public float Caloricity 
        {
            get { return Components.Sum(comp => comp.Caloricity); }
        }

        public float Weight 
        {
            get { return Components.Sum(comp => comp.Weight); }
        }
    }
}
