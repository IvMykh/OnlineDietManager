using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.DishesManagement.Abstract
{
    public abstract class DishComponent
        : IDishComponent
    {
        public string   Name            { get; set; }
        public string   Description     { get; set; }

        public float    Protein         { get; protected set; }
        public float    Fat             { get; protected set; }
        public float    Carbohydrates   { get; protected set; }
        public float    Caloricity      { get; protected set; }
        public float    Weight          { get; protected set; }
    }
}
