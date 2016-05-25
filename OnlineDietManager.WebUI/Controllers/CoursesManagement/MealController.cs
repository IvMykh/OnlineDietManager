using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.CoursesManagement;

namespace OnlineDietManager.WebUI.Controllers
{
    public class MealController : Controller
    {
        private IUnitOfWork uow;
        
        public MealController(IUnitOfWork uow_)
        {
            uow = uow_;
        }
        
        // GET:
        public ActionResult Index(int dayRefId, string returnUrl)
        {
            var meals = uow.MealsRepository.GetAll()
                            .Where(m => m.Day_ID == dayRefId)
                            .ToList<Meal>();

            return PartialView("_ListMealsForDayPartial",
                new ListMealsForDayViewModel {
                    DayId = dayRefId,
                    Meals = meals,
                    ReturnUrl = returnUrl
                });
        }

        [HttpGet]
        public ActionResult Create(string returnUrl_)
        {
            var model = new MealViewModel
            {
                Meal = new Domain.CoursesManagement.Meal(),
                ReturnUrl = returnUrl_
            };

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id, string returnUrl, int dayId)
        {
            var mealToEdit = uow.MealsRepository.
                GetAll().
                FirstOrDefault(meal => meal.ID == id);

            return View(new MealViewModel
            {
                Meal = mealToEdit,
                ReturnUrl = returnUrl,
                DayId = dayId
            });
        }

        [HttpPost]
        public ActionResult Edit(MealViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Meal.ID == 0)
                {
                    model.Meal.Day_ID = model.DayId;
                    uow.MealsRepository.Insert(model.Meal);
                }
                else
                {
                    uow.MealsRepository.Update(model.Meal);
                }

                uow.Save();
                TempData["message"] = "meal was saved";

                if (model.ReturnUrl!= null)
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Delete(MealViewModel model)
        {
            // TODO: implement.
            throw new NotImplementedException();
        }
    }
}