using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class ActiveCoursesController : Controller
    {
        IUnitOfWork UnitOfWork;
        // GET: ActiveCourses
        public ActiveCoursesController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Launch(int idToLaunch, string returnUrl)
        {
            var courseToLaunch = UnitOfWork.CoursesRepository.GetById(idToLaunch);
            var user = User.Identity.GetUserId();
            var presentActive = UnitOfWork.ActiveCoursesRepository.
                GetAll().
                Where(course => course.OwnerID == user).
                ToList<Course>();

            if (presentActive.Count != 0)
            {
                UnitOfWork.ActiveCoursesRepository.Delete(presentActive[0].ID);
                UnitOfWork.Save();
            }

            var launchedCourse = new ActiveCourse
            {
                Days = new List<Day>(),
                Description = courseToLaunch.Description,
                OwnerID = user,
                StartDate = DateTime.Now.Date
            };

            foreach (var day in courseToLaunch.Days)
            {
                var dayCopy = new Day
                {
                    Description = day.Description,
                    Meals = new List<Meal>()
                };

                foreach (var meal in day.Meals)
                {
                    var mealCopy = new Meal
                    {
                        Description = meal.Description,
                        Time = meal.Time,
                        Dishes = new List<Dish>()
                    };

                    foreach (var dish in meal.Dishes)
                    {
                        mealCopy.Dishes.Add(dish);
                    }

                    dayCopy.Meals.Add(mealCopy);
                }

                launchedCourse.Days.Add(dayCopy);
            }

            UnitOfWork.ActiveCoursesRepository.Insert(launchedCourse);
            UnitOfWork.Save();

            TempData["message"] = string.Format(
                    "{0} has been successfully launched", idToLaunch);

            return Redirect(returnUrl);
        }
    }
}