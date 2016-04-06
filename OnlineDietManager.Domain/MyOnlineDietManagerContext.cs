using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using OnlineDietManager.Domain.Entities.DishesManagement;
using OnlineDietManager.Domain.Entities.UsersManagement;
using OnlineDietManager.Domain.Entities.CoursesManagement;

namespace OnlineDietManager.Domain.Practice
{
    public class MyOnlineDietManagerContext
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
