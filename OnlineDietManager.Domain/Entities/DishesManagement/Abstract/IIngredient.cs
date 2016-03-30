using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.DishesManagement.Abstract
{
    public interface IIngredient
        : IDishComponent
    {
        new float Protein       { get; set; }
        new float Fat           { get; set; }
        new float Carbohydrates { get; set; }
        new float Caloricity    { get; set; }
        new float Weight        { get; set; }
    }
}
