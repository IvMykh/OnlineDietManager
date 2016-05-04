using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineDietManager.Domain;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;

namespace OnlineDietManager.DataManip
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var odmUnitOfWork = new ODMUnitOfWork())
            {
                string ownerId = "5fc31c2b-41f0-4bf7-8e5a-af286850e56f";

                var course = new Course {
                    Description = "Sample course",
                    OwnerID = ownerId,
                    Days = new List<Day>()
                };

                int courseDuration = 4;
                for (int i = 0; i < courseDuration; i++)
                {
                    var day = new Day() { 
                        Description = string.Format("Day #{0}", i + 1), 
                        Meals = new List<Meal>() 
                    };

                    IEnumerable<Dish> usersDishes = odmUnitOfWork.DishesRepository
                                                        .GetAll()
                                                        .Where(d => d.OwnerID == ownerId)
                                                        .ToList();

                    var dishesList = new List<Dish> { usersDishes.First(), usersDishes.Skip(1).First() };
                    var breakfast = new Meal { 
                        Description = "Breakfast", 
                        Time = new TimeSpan(9, 0, 0),
                        Dishes = dishesList
                    };

                    dishesList = new List<Dish> { usersDishes.Skip(1).First(), usersDishes.Skip(2).First() };
                    var dinner = new Meal {
                        Description = "Dinner",
                        Time = new TimeSpan(13, 0, 0),
                        Dishes = dishesList
                    };

                    day.Meals.Add(breakfast);
                    day.Meals.Add(dinner);

                    course.Days.Add(day);
                    odmUnitOfWork.CoursesRepository.Insert(course);
                }
                
                odmUnitOfWork.Save();

            }
        }
    }
}
