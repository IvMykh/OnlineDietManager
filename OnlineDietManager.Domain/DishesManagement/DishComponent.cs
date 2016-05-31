using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        [Required(ErrorMessage = "Please, specify corresponding ingredient")]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [ForeignKey("DishRefID")]
        public virtual Dish Dish { get; set; }

        [Key, Column(Order = 1)]
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Please, specify corresponding dish")]
        public int DishRefID { get; set; }

        [Required(ErrorMessage = "Please, specify a weight of component")]
        [Column(Order = 2)]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be positive")]
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
