using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UsersManagement;

namespace OnlineDietManager.Domain.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Ingredient> IngredientsRepository { get; }
        IRepository<DishComponent> DishComponentsRepository { get; }
        IRepository<Dish> DishesRepository { get; }
        IRepository<User> UsersRepository { get; }
        IRepository<Meal> MealsRepository { get; }
        IRepository<Day> DaysRepository { get; }
        IRepository<Course> CoursesRepository { get; }
    }
}
