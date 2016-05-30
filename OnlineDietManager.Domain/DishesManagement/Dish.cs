using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.CoursesManagement;

namespace OnlineDietManager.Domain.DishesManagement
{
    [Table("Dishes")]
    public class Dish
        : FoodComponent
    {
        [Key, Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[Required]
        public virtual IList<DishComponent> Components
        {
            get;
            set;
        }

        //[Required]
        public virtual IList<Meal> Meals
        {
            get;
            set;
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

        public Dish CopyFor(string ownerId)
        {
            Dish dishPersonalCopy = new Dish {
                ID          = this.ID,
                Name        = this.Name,
                Description = this.Description,
                Meals       = new List<Meal>(),
                OwnerID     = ownerId,
            };


            foreach (DishComponent comp in this.Components)
            {
                Ingredient ingredientPersonalCopy = comp.Ingredient.CopyFor(ownerId);

                dishPersonalCopy.Components.Add(new DishComponent {
                    Dish        = dishPersonalCopy,
                    Ingredient  = ingredientPersonalCopy,
                    Weight      = comp.Weight
                });
            }

            return dishPersonalCopy;
        }
    }
}
