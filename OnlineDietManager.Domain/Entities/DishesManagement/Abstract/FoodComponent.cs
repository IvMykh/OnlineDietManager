using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;

namespace OnlineDietManager.Domain.Entities.DishesManagement.Abstract
{
    public abstract class FoodComponent
    {
        [Required, Column(Order = 1)]
        public string   Name            { get; set; }

        [Required, Column(Order = 2)]
        public string   Description     { get; set; }
    }
}
