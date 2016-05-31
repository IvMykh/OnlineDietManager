using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;
using Microsoft.AspNet.Identity;

namespace OnlineDietManager.WebUI.Controllers
{
    // Contains several template methods.
    public abstract class AbstrDishesController 
        : Controller
    {
        protected IUnitOfWork OdmUnitOfWork { get; private set; }

        public AbstrDishesController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        protected abstract ViewResult GetViewResultFor(string viewName = null, object model = null);
        protected abstract RedirectToRouteResult GetRedirectToActionFor(string actionName, object routeParams = null);

        protected abstract object GetIndexModel();
        protected abstract string GetOwnerName();


        public ActionResult Index()
        {
            return GetViewResultFor("Index", GetIndexModel()); // View(model);
        }

        public ActionResult Create(string returnUrl)
        {
            var model = new DishViewModel
            {
                Dish = new Dish(),
                ReturnUrl = returnUrl
            };

            return GetViewResultFor("Create", model); //View(model);
        }

        public ActionResult Create(DishViewModel newDishVM)
        {
            if (ModelState.IsValid)
            {
                string userId = GetOwnerName();
                newDishVM.Dish.OwnerID = userId;

                OdmUnitOfWork.DishesRepository.Insert(newDishVM.Dish);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been saved", newDishVM.Dish.Name);

                return GetRedirectToActionFor("Edit", new
                    {
                        Id = newDishVM.Dish.ID,
                        returnUrl = newDishVM.ReturnUrl
                    });
            }
            else
            {
                return GetViewResultFor("Create", newDishVM);// View(newDishVM);
            }
        }


        public ActionResult Edit(int Id, string returnUrl)
        {
            var dishToEdit = OdmUnitOfWork.DishesRepository
                                .GetAll()
                                .FirstOrDefault(dish => dish.ID == Id);

            return GetViewResultFor("Edit", new DishViewModel
                {
                    Dish = dishToEdit,
                    ReturnUrl = returnUrl
                });
            //View(new DishViewModel
            //{
            //    Dish = dishToEdit,
            //    ReturnUrl = returnUrl
            //});
        }

        public ActionResult Edit(DishViewModel dishVM)
        {
            if (ModelState.IsValid)
            {
                OdmUnitOfWork.DishesRepository.Update(dishVM.Dish);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been saved", dishVM.Dish.Name);

                if (dishVM.ReturnUrl != null)
                {
                    return Redirect(dishVM.ReturnUrl);
                }

                return GetRedirectToActionFor("Index"); //RedirectToAction("Index");
            }
            else
            {
                return GetViewResultFor("Edit", dishVM); //View(dishVM);
            }
        }

        public ActionResult Delete(int Id, string returnUrl)
        {
            Dish dishToDelete = OdmUnitOfWork.DishesRepository
                                    .GetById(Id);

            if (dishToDelete != null)
            {
                OdmUnitOfWork.DishesRepository.Delete(Id);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been successfully deleted", dishToDelete.Name);
            }

            return Redirect(returnUrl);
        }


        public ActionResult ViewNutritionalSummary(int dishId)
        {
            Dish dish = OdmUnitOfWork.DishesRepository.GetById(dishId);
            return PartialView("_NutritionalSummaryPartial",
                new NutritionalSummary
                {
                    PanelCaption    = "Nutritional Summary",
                    Protein         = dish.Protein,
                    Fat             = dish.Fat,
                    Carbohydrates   = dish.Carbohydrates,
                    Caloricity      = dish.Caloricity,
                    Weight          = dish.Weight
                });
        }
	}
}