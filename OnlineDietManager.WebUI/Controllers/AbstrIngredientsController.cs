using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    // Contains several template methods.
    public abstract class AbstrIngredientsController
        : Controller
    {
        protected IUnitOfWork OdmUnitOfWork { get; private set; }

        public AbstrIngredientsController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        protected abstract ViewResult GetViewResultFor(string viewName = null, object model = null);
        protected abstract RedirectToRouteResult GetRedirectToActionFor(string viewName);

        protected abstract object GetIndexModel();
        protected abstract string GetUserNameForEdit();

        public ActionResult Create(string returnUrl)
        {
            var model = new IngredientViewModel
                {
                    Ingredient = new Ingredient(),
                    ReturnUrl = returnUrl
                };

            return GetViewResultFor("Edit", model); //View("Edit", model);
        }

        public ActionResult Index()
        {
            return GetViewResultFor("Index", GetIndexModel());  // View(model);
        }

        public ActionResult Edit(int Id, string returnUrl)
        {
            var ingredientToEdit = OdmUnitOfWork.IngredientsRepository
                                    .GetAll()
                                    .FirstOrDefault(ing => ing.ID == Id);

            return View(new IngredientViewModel
            {
                Ingredient = ingredientToEdit,
                ReturnUrl = returnUrl
            });
        }

        public ActionResult Edit(IngredientViewModel ingredientVM)
        {
            if (ModelState.IsValid)
            {
                if (ingredientVM.Ingredient.ID == 0)
                {
                    ingredientVM.Ingredient.OwnerID = GetUserNameForEdit();
                    OdmUnitOfWork.IngredientsRepository.Insert(ingredientVM.Ingredient);
                }
                else
                {
                    OdmUnitOfWork.IngredientsRepository.Update(ingredientVM.Ingredient);
                }

                OdmUnitOfWork.Save();
                TempData["message"] = string.Format(
                    "{0} has been saved", ingredientVM.Ingredient.Name);

                if (ingredientVM.ReturnUrl != null)
                {
                    return Redirect(ingredientVM.ReturnUrl);
                }

                return GetRedirectToActionFor("Index"); //RedirectToAction("Index");
            }
            else
            {
                return GetViewResultFor("Edit", ingredientVM); //View(ingredientVM);
            }
        }

        public ActionResult Delete(int Id, string returnUrl)
        {
            Ingredient ingredientToDelete = OdmUnitOfWork.IngredientsRepository
                                                .GetById(Id);

            if (ingredientToDelete != null)
            {
                OdmUnitOfWork.IngredientsRepository.Delete(Id);
                OdmUnitOfWork.Save();
                TempData["message"] = string.Format(
                    "{0} has been successfully deleted", ingredientToDelete.Name);
            }

            return Redirect(returnUrl);
        }
    }
}