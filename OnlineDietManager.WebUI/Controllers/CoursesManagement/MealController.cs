using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class MealController : Controller
    {
        private IUnitOfWork uow;
        public MealController(IUnitOfWork uow_)
        {
            uow = uow_;
        }
        // GET: Meal
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(string returnUrl_)
        {
            var model = new MealViewModel
            {
                Meal = new Domain.CoursesManagement.Meal(),
                returnUrl = returnUrl_
            };

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int Id, string returnUrl_, int Day_ID_)
        {
            var mealToEdit = uow.MealsRepository.
                GetAll().
                FirstOrDefault(meal => meal.ID == Id);

            return View(new MealViewModel
            {
                Meal = mealToEdit,
                returnUrl = returnUrl_,
                Day_ID = Day_ID_
            });
        }

        [HttpPost]
        public ActionResult Edit(MealViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Meal.ID == 0)
                {
                    model.Meal.Day_ID = model.Day_ID;
                    uow.MealsRepository.Insert(model.Meal);
                }
                else
                {
                    uow.MealsRepository.Update(model.Meal);
                }

                uow.Save();
                TempData["message"] = "meal was saved";

                if (model.returnUrl!= null)
                {
                    return Redirect(model.returnUrl);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}