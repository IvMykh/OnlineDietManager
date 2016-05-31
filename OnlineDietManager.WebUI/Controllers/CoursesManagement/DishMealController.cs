using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using OnlineDietManager.WebUI.Models;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.DishesManagement;

namespace OnlineDietManager.WebUI.Controllers.CoursesManagement
{
    public class DishMealController : Controller
    {
        private IUnitOfWork OdmUnitOfWork { get; set; }

        public DishMealController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult AddNew(int dishId, int mealRefId, string returnUrl)
        {
            Meal referedMeal = OdmUnitOfWork.MealsRepository.GetById(mealRefId);
            Dish referedDish = OdmUnitOfWork.DishesRepository.GetById(dishId);

            referedMeal.Dishes.Add(referedDish);
            OdmUnitOfWork.Save();

            return Redirect(returnUrl);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult ListAvailableDishes(int mealRefId, string returnUrl, OwnerPolicy ownerPolicy)
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<Dish> model = null;

            switch (ownerPolicy)
            {
                case OwnerPolicy.UserOnly:
                    {
                        model = OdmUnitOfWork.DishesRepository
                                .GetAll()
                                .Where(d => d.OwnerID == userId);
                    } break;
                case OwnerPolicy.GlobalOnly:
                    {
                        model = OdmUnitOfWork.DishesRepository
                                .GetAll()
                                .Where(d => d.OwnerID == null);
                    } break;
                case OwnerPolicy.Both:
                    {
                        model = OdmUnitOfWork.DishesRepository
                                .GetAll()
                                .Where(d => d.OwnerID == userId || d.OwnerID == null);
                    } break;

                default: throw new ArgumentException(
                    string.Format("'{0}': unexpected owner policy type"));
            }

            return PartialView("_ListAvailableDishesPartial", 
                model.OrderBy(d => d.Name)
                     .Select(dish => new SelectDishViewModel
                     {
                         Dish = dish,
                         MealRefId = mealRefId,
                         ReturnUrl = returnUrl
                     }));
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult IndexIncludedDishes(int mealRefId, string returnUrl)
        {
            Meal meal = OdmUnitOfWork.MealsRepository.GetById(mealRefId);

            var ownerPolicy = meal.Day.Course.OwnerID == null ?
                                    OwnerPolicy.GlobalOnly :
                                    OwnerPolicy.UserOnly;

            return PartialView("_ListIncludedDishesPartial", new ListIncludedDishesViewModel {
                IncludedDishes = meal.Dishes,
                MealRefId = mealRefId,
                ReturnUrl = returnUrl,
                OwnerPolicy = ownerPolicy
            });
        }

        [HttpPost]
        public ActionResult Delete(int dishId, int mealRefId, string returnUrl)
        {
            Meal referedMeal = OdmUnitOfWork.MealsRepository.GetById(mealRefId);
            Dish referedDish = OdmUnitOfWork.DishesRepository.GetById(dishId);

            referedMeal.Dishes.Remove(referedDish);
            OdmUnitOfWork.Save();

            return Redirect(returnUrl);
        }
	}
}