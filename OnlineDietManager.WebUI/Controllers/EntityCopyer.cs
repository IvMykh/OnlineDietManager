using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;

namespace OnlineDietManager.WebUI.Controllers
{
    public class EntityCopyer
    {
        public static EntityCopyer Instance { get; private set; }

        static EntityCopyer()
        {
            Instance = new EntityCopyer();
        }

        private EntityCopyer()
        {
        }

        public Course CopyCourse(Course sourceCourse, string ownerId, IUnitOfWork odmUnitOfWork)
        {
            var courseCopy = new Course()
            {
                ID = sourceCourse.ID,
                Description = sourceCourse.Description,
                OwnerID = ownerId
            };

            var diffDishesCopies =
                sourceCourse.Days
                    .SelectMany(d => d.Meals)
                    .SelectMany(m => m.Dishes)
                    .Distinct()
                    .Select(d => d.CopyFor(ownerId)).ToList();

            foreach (var dish in diffDishesCopies)
            {
                odmUnitOfWork.DishesRepository.Insert(dish);
            }

            List<Day> daysCopies = new List<Day>();

            foreach (var day in sourceCourse.Days)
            {
                var dayCopy = new Day
                {
                    ID = day.ID,
                    Description = day.Description,
                    Meals = new List<Meal>()
                };

                foreach (var meal in day.Meals)
                {
                    var mealCopy = new Meal
                    {
                        ID = meal.ID,
                        Description = meal.Description,
                        Time = meal.Time,
                        Day = dayCopy,
                        Dishes = new List<Dish>()
                    };

                    foreach (var dish in meal.Dishes)
                    {
                        mealCopy.Dishes.Add(diffDishesCopies.Where(d => d.ID == dish.ID).First());
                    }

                    dayCopy.Meals.Add(mealCopy);
                }

                daysCopies.Add(dayCopy);
            }

            courseCopy.Days = daysCopies;

            return courseCopy;
        }
    }
}