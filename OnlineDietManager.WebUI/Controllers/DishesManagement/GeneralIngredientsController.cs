using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    public class GeneralIngredientsController 
        : AbstrIngredientsController
    {
        public GeneralIngredientsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override ViewResult GetViewResultFor(string viewName = null, object model = null)
        {
            if (viewName == null && model == null)
                return View();

            if (viewName == null)
                return View(model);

            if (model == null)
                return View(viewName);

            return View(viewName, model);
        }
        protected override RedirectToRouteResult GetRedirectToActionFor(string viewName)
        {
            return RedirectToAction(viewName);
        }

        protected override object GetIndexModel()
        {
            IEnumerable<Ingredient> generalIngredients =
                OdmUnitOfWork.IngredientsRepository
                            .GetAll()
                            .Where(ing => ing.OwnerID == null)
                            .OrderBy(ing => ing.Name)
                            .ToList<Ingredient>();
            
            return generalIngredients;
        }
        protected override string GetUserNameForEdit()
        {
            return null;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public new ActionResult Create(string returnUrl)
        {
            return base.Create(returnUrl);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public new ActionResult Edit(int Id, string returnUrl)
        {
            return base.Edit(Id, returnUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Edit(IngredientViewModel ingredientVM)
        {
            return base.Edit(ingredientVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Delete(int Id, string returnUrl)
        {
            return base.Delete(Id, returnUrl);
        }

        [HttpPost]
        public ActionResult AddToPersonal(int Id, string returnUrl)
        {
            Ingredient ingredientToAdd = OdmUnitOfWork.IngredientsRepository.GetById(Id);

            if (ingredientToAdd != null)
            {
                Ingredient personalCopy = new Ingredient
                    {
                        Name            = ingredientToAdd.Name,
                        Description     = ingredientToAdd.Description,
                        Protein         = ingredientToAdd.Protein,
                        Fat             = ingredientToAdd.Fat,
                        Carbohydrates   = ingredientToAdd.Carbohydrates,
                        Caloricity      = ingredientToAdd.Caloricity,
                        OwnerID         = User.Identity.GetUserId()
                    };

                OdmUnitOfWork.IngredientsRepository.Insert(personalCopy);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been successfully added to personal ingredients", ingredientToAdd.Name);
            }

            return Redirect(returnUrl);
        }
	}
}