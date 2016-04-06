using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;
using OnlineDietManager.Domain.Entities.Abstract;

namespace OnlineDietManager.Domain.Entities.DishesManagement.Abstract
{
    public abstract class FoodComponent 
        : IDescribable
    {
        [Required, Column(Order = 1)]
        public string   Name            { get; set; }

        [Required, Column(Order = 2)]
        public string   Description     { get; set; }
    }
}
