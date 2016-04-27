using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI
{
    //public class OnlineDietManagerContext
    //    : IdentityDbContext<AppUser>
    //{
    //    public OnlineDietManagerContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public DbSet<Ingredient> Ingredients { get; set; }
    //    public DbSet<DishComponent> DishComponents { get; set; }
    //    public DbSet<Dish> Dishes { get; set; }
    //    //public DbSet<User> Users { get; set; }
    //    public DbSet<Meal> Meals { get; set; }
    //    public DbSet<Day> Days { get; set; }
    //    public DbSet<Course> Courses { get; set; }
    //}
}