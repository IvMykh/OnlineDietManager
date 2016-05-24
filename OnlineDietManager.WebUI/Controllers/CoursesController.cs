using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class CoursesController : Controller
    {
        private IUnitOfWork UnitOfWork { get; set; }

        public CoursesController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        // GET: Courses
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var res = new List<Course>();
            var model = UnitOfWork.CoursesRepository.
                GetAll().
                Where(course => course.OwnerID == user);
            res.AddRange(model);

            model = UnitOfWork.CoursesRepository.
                GetAll().
                Where(course => course.OwnerID == null);

            res.AddRange(model);

            ViewBag.isAdmin = User.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin));
            ViewBag.UserId = user;

            return View("Index", res);
        }

        [HttpPost]
        public ActionResult AddToPersonal(int Id, string returnUrl)
        {
            Course courseToAdd = UnitOfWork.CoursesRepository.GetById(Id);

            if (courseToAdd != null)
            {
                Course coursePersonalCopy = new Course
                {
                    Days = new List<Day>(),
                    Description = courseToAdd.Description,
                    OwnerID = User.Identity.GetUserId()
                };


                foreach (var day in courseToAdd.Days)
                {
                    var dayCopy = new Day
                    {
                        Description = day.Description,
                        Meals = new List<Meal>()
                    };

                    foreach(var meal in day.Meals)
                    {
                        var mealCopy = new Meal
                        {
                            Description = meal.Description,
                            Time = meal.Time,
                            Dishes = new List<Dish>()
                        };

                        foreach(var dish in meal.Dishes)
                        {
                            mealCopy.Dishes.Add(dish);
                        }

                        dayCopy.Meals.Add(mealCopy);
                    }

                    coursePersonalCopy.Days.Add(dayCopy);
                }

                UnitOfWork.CoursesRepository.Insert(coursePersonalCopy);
                UnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been successfully added to personal courses", courseToAdd.ID);
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult Delete(int Id, string returnUrl)
        {
            Course courseToDelete = UnitOfWork.CoursesRepository
                                    .GetById(Id);

            if (courseToDelete != null)
            {
                UnitOfWork.CoursesRepository.Delete(Id);
                UnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been successfully deleted", courseToDelete.Description);
            }

            return Redirect(returnUrl);
        }
    }
}