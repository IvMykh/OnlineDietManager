using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineDietManager.Domain.Entities.DishesManagement;
using OnlineDietManager.Domain.Entities.DishesManagement.Abstract;

namespace OnlineDietManager.Domain.Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyOnlineDietManagerContext())
            {
                //var ingredient = new Ingredient()
                //    {
                //        Name            = "Orange juice",
                //        Description     = "The great orange juice",
                //        Protein         = 5.5f,
                //        Fat             = 9.5f,
                //        Carbohydrates   = 35.7f,
                //        Caloricity      = 55
                //    };
                //
                //
                //var dishComp = new DishComponent
                //    {
                //        Ingredient  = ingredient,
                //        Weight      = 25.7f
                //    };
                //
                //var dish = new Dish()
                //    {
                //        Name = "Cake",
                //        Description = "Very taste cake",
                //    };
                //
                //dish.Components.Add(dishComp);
                //
                //db.Ingredients.Add(ingredient);
                //db.Dishes.Add(dish);


                var ingredients = from ing in db.Ingredients
                                  select ing;

                var ingredientsStrFormat = "{0,20} {1,10} {2,10} {3,20} {4,10}";

                Console.WriteLine("Ingredients:");
                Console.WriteLine(ingredientsStrFormat, "Name", "Protein", "Fat", "Carbohydrates", "Caloricity");
                foreach (var ing in ingredients)
                {
                    Console.WriteLine(ingredientsStrFormat, 
                        ing.Name, 
                        ing.Protein,
                        ing.Fat,
                        ing.Carbohydrates,
                        ing.Caloricity);
                }

                var dishes = from dish in db.Dishes
                             select dish;

                Console.WriteLine("Dishes:");
                foreach (var d in dishes)
                {
                    Console.WriteLine("{0,10} {1,10} {2,10} {3,15} {4,10} {5,10}",
                        d.Name,
                        d.Protein,
                        d.Fat,
                        d.Carbohydrates,
                        d.Caloricity,
                        d.Weight);

                    Console.WriteLine("Components:");

                    foreach (var comp in d.Components)
                    {
                        Console.WriteLine("{0,10} {1,10} {2,10} {3,15} {4,10} {5,10}",
                            comp.Ingredient.Name,
                            comp.Protein,
                            comp.Fat,
                            comp.Carbohydrates,
                            comp.Caloricity,
                            comp.Weight);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
