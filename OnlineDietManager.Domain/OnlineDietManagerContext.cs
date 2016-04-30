using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UsersManagement;

namespace OnlineDietManager.Domain
{
    public class OnlineDietManagerContext
        : IdentityDbContext<AppUser>
    {
        public static OnlineDietManagerContext Create()
        {
            return new OnlineDietManagerContext();
        }
        public OnlineDietManagerContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishComponent> DishComponents { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Course> Courses { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DishComponent>()
        //        .HasRequired(a => a.)
        //        .WithRequiredDependent(a => a.)
        //        .WillCascadeOnDelete(true);
        //}
    }
}
