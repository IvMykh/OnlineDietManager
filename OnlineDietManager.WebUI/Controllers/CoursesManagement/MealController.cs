using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    public class MealController : Controller
    {
        private IUnitOfWork odmUnitOfWork { get; set; }
        
        public MealController(IUnitOfWork uow)
        {
            odmUnitOfWork = uow;
        }
        
        [HttpGet]
        public ActionResult Index(int dayRefId, string returnUrl)
        {
            var meals = odmUnitOfWork.MealsRepository.GetAll()
                            .Where(m => m.Day_ID == dayRefId)
                            .ToList<Meal>();

            return PartialView("_ListMealsForDayPartial",
                new ListMealsForDayViewModel {
                    DayId = dayRefId,
                    Meals = meals,
                    ReturnUrl = returnUrl
                });
        }

        [HttpPost]
        public ActionResult Create(int dayId, string returnUrl)
        {
            odmUnitOfWork.MealsRepository.Insert(new Meal {
                 Day_ID = dayId,
                 Time = new TimeSpan(0, 0, 0),
                 Dishes = new List<Dish>()
            });
            odmUnitOfWork.Save();

            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult Edit(int id, string returnUrl)
        {
            var mealToEdit = odmUnitOfWork.MealsRepository.GetById(id);
            return View(new MealViewModel {
                Meal = mealToEdit,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public ActionResult Edit(MealViewModel mealVM)
        {
            if (ModelState.IsValid)
            {
                odmUnitOfWork.MealsRepository.Update(mealVM.Meal);
                odmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Meal '{0}' has been saved", mealVM.Meal.ID);

                return Redirect(mealVM.ReturnUrl);
            }
            else
            {
                return View("Edit", mealVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int mealId, string returnUrl)
        {
            odmUnitOfWork.MealsRepository.Delete(mealId);
            odmUnitOfWork.Save();

            return Redirect(returnUrl);
        }

        
    }
}