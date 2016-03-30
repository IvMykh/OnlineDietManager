using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.DishesManagement.Abstract
{
    public interface IDishComponent
    {
        string  Name        { get; set; }
        string  Description { get; set; }

        float   Protein         { get; }
        float   Fat             { get; }
        float   Carbohydrates   { get; }
        float   Caloricity      { get; }
        float   Weight          { get; }
    }
}
