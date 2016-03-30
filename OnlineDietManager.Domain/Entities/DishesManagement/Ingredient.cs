using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineDietManager.Domain.Entities.DishesManagement.Abstract;

namespace OnlineDietManager.Domain.Entities.DishesManagement
{
    public class Ingredient
        : DishComponent, IIngredient
    {
        public new float Protein
        {
            get { return base.Protein; }
            set { base.Protein = value; }
        }

        public new float Fat             
        {
            get { return base.Fat; }
            set { base.Fat = value; }
        }
        
        public new float Carbohydrates   
        {
            get { return base.Carbohydrates; }
            set { base.Carbohydrates = value; }
        }
        
        public new float Caloricity      
        {
            get { return base.Caloricity; }
            set { base.Caloricity = value; }
        }
        
        public new float Weight 
        {
            get { return base.Weight; }
            set { base.Weight = value; }
        }
    }
}
