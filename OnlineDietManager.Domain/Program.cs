using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineDietManager.Domain.Entities.DishesManagement;
using OnlineDietManager.Domain.Entities.DishesManagement.Abstract;

namespace OnlineDietManager.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            IIngredient ing1 = new Ingredient 
                { 
                    Name = "Egg", 
                    Description = "The Egg", 
                    Protein = 35.0f 
                };
            
            IIngredient ing2 = new Ingredient
            {
                Name = "Milk",
                Description = "The Milk",
                Protein = 50.0f
            };

            IDish dish1 = new Dish() 
                { 
                    Name = "MyDish", 
                    Description = "The MyDish" 
                };

            dish1.AddIngredient(ing1);
            dish1.AddIngredient(ing2);
            Console.WriteLine(dish1.Protein);

            dish1.RemoveIngredient(ing2);
            Console.WriteLine(dish1.Protein);

            dish1.RemoveIngredient(ing1);
            Console.WriteLine(dish1.Protein);
        }
    }
}
