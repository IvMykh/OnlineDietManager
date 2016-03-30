using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineDietManager.Domain.Entities.DishesManagement.Abstract;

namespace OnlineDietManager.Domain.Entities.DishesManagement
{
    public class Dish
        : DishComponent, IDish
    {
        private IList<IIngredient> _ingredients;

        public Dish()
        {
            _ingredients = new List<IIngredient>();
        }

        public new float Protein
        {
            get         { return _ingredients.Sum(ing => ing.Protein); }
            private set { base.Protein = value; }
        }

        public new float Fat 
        { 
            get         { return _ingredients.Sum(ing => ing.Fat); }
            private set { base.Fat = value; }
        }

        public new float Carbohydrates 
        {
            get         { return _ingredients.Sum(ing => ing.Carbohydrates); }
            private set { base.Carbohydrates = value; } 
        }

        public new float Caloricity 
        {
            get { return _ingredients.Sum(ing => ing.Caloricity); }
            private set { base.Caloricity = value; }
        }

        public new float Weight 
        {
            get { return _ingredients.Sum(ing => ing.Weight); }
            private set { base.Weight = value; }
        }

        private void addCharacteristics(IIngredient ingredient)
        {
            Protein         += ingredient.Protein;
            Fat             += ingredient.Fat;
            Carbohydrates   += ingredient.Carbohydrates;
            Caloricity      += ingredient.Caloricity;
            Weight          += ingredient.Weight;
        }

        private void subtractCharacteristics(IIngredient ingredient)
        {
            Protein         -= ingredient.Protein;
            Fat             -= ingredient.Fat;
            Carbohydrates   -= ingredient.Carbohydrates;
            Caloricity      -= ingredient.Caloricity;
            Weight          -= ingredient.Weight;
        }

        public void AddIngredient(IIngredient ingredient)
        {
            _ingredients.Add(ingredient);
            addCharacteristics(ingredient);
        }

        public void RemoveIngredient(IIngredient ingredient)
        {
            bool isRemoved = _ingredients.Remove(ingredient);
            if (isRemoved)
            {
                subtractCharacteristics(ingredient);
            }
        }
    }
}
