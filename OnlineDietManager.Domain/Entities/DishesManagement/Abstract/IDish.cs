﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.DishesManagement.Abstract
{
    public interface IDish
        : IDishComponent
    {
        void AddIngredient(IIngredient ingredient);
        void RemoveIngredient(IIngredient ingredient);
    }
}
