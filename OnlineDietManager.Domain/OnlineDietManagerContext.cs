﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UsersManagement;

namespace OnlineDietManager.Domain
{
    public class OnlineDietManagerContext
        : DbContext
    {
        public DbSet<Ingredient>    Ingredients     { get; set; }
        public DbSet<DishComponent> DishComponents  { get; set; }
        public DbSet<Dish>          Dishes          { get; set; }
        public DbSet<User>          Users           { get; set; }
        public DbSet<Meal>          Meals           { get; set; }
        public DbSet<Day>           Days            { get; set; }
        public DbSet<Course>        Courses         { get; set; }
    }
}