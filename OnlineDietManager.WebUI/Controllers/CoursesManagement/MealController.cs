using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.CoursesManagement;
using Microsoft.AspNet.Identity;

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
        public ActionResult Create(string returnUrl_, int dayId)
        {
            var allDishes = uow.DishesRepository.
                GetAll().
                Where(dish => dish.OwnerID == User.Identity.GetUserId());

            var model = new MealViewModel
            {
                Meal = new Domain.CoursesManagement.Meal(),
                ReturnUrl = returnUrl_,
                DayId = dayId,
                AllDishes = allDishes,
                SelectedDishId = 0
            };

            return View("Edit", model);
        }

        [HttpGet]
        public ActionResult Edit(int id, string returnUrl, int dayId, int selectedDishId = 0)
        {
            var mealToEdit = uow.MealsRepository.
                GetAll().
                FirstOrDefault(meal => meal.ID == id);
            var userId = User.Identity.GetUserId();
            var allDishes = uow.DishesRepository.
                GetAll().
                Where(dish => dish.OwnerID == userId);
            
            return View(new MealViewModel
            {
                Meal = mealToEdit,
                ReturnUrl = returnUrl,
                DayId = dayId,
                AllDishes = allDishes,
                SelectedDishId = selectedDishId
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

                if(model.SelectedDishId != 0)
                {
                    model.Meal.Dishes.Add(uow.DishesRepository
                        .GetById(model.SelectedDishId));

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