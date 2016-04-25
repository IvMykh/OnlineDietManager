using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.Abstract;

namespace OnlineDietManager.Domain.DishesManagement
{
    [Table("DishComponents")]
    public class DishComponent
        : IDescribable
    {
        [ForeignKey("ID")]
        virtual public Ingredient Ingredient { get; set; }

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ForeignKey("DishRefID")]
        public virtual Dish Dish { get; set; }

        [Key, Column(Order = 1)]
        public int DishRefID { get; set; }

        [Required, Column(Order = 2)]
        public float Weight { get; set; }

        public float Protein
        {
            get { return Ingredient.Protein / SpecialData.STANDARD_PORTION * Weight; }
        }
        public float Fat
        {
            get { return Ingredient.Fat / SpecialData.STANDARD_PORTION * Weight; }
        }
        public float Carbohydrates
        {
            get { return Ingredient.Carbohydrates / SpecialData.STANDARD_PORTION * Weight; }
        }
        public float Caloricity
        {
            get { return Ingredient.Caloricity / SpecialData.STANDARD_PORTION * Weight; }
        }
        public string Description
        {
            get { return Ingredient.Description; }
        }
    }
}
