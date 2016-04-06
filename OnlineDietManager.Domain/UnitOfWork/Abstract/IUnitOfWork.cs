using OnlineDietManager.Domain.Entities.CoursesManagement;
using OnlineDietManager.Domain.Entities.DishesManagement;
using OnlineDietManager.Domain.Entities.UsersManagement;
using OnlineDietManager.Domain.Repositories.Abstract;

namespace OnlineDietManager.Domain.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Ingredient>     IngredientsRepository       { get; }
        IRepository<DishComponent>  DishComponentsRepository    { get; }
        IRepository<Dish>           DishesRepository            { get; }
        IRepository<User>           UsersRepository             { get; }
        IRepository<Meal>           MealsRepository             { get; }
        IRepository<Day>            DaysRepository               { get; }
        IRepository<Course>         CoursesRepository           { get; }
    }
}
