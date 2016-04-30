using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    public class IngredientsController : Controller
    {
        private IUnitOfWork odmUnitOfWork;

        public IngredientsController(IUnitOfWork unitOfWork)
        {
            odmUnitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            return View("Edit", new IngredientViewModel
                {
                    Ingredient = new Ingredient(),
                    ReturnUrl = returnUrl
                });
        }

        // ...

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            return View(odmUnitOfWork.IngredientsRepository
                        .GetAll()
                        .Where(ing => ing.OwnerID == userId)
                        .ToList<Ingredient>());
        }

        [HttpGet]
        public ActionResult Edit(int Id, string returnUrl)
        {
            string userId = User.Identity.GetUserId();

            var ingredientToEdit = odmUnitOfWork.IngredientsRepository
                                    .GetAll()
                                    .Where(ing => ing.OwnerID == userId)
                                    .FirstOrDefault(ing => ing.ID == Id);

            return View(new IngredientViewModel
                {
                    Ingredient = ingredientToEdit,
                    ReturnUrl = returnUrl
                });
        }

        [HttpPost]
        public ActionResult Edit(IngredientViewModel ingredientVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ingredientVM.Ingredient.OwnerID))
                {
                    ingredientVM.Ingredient.OwnerID = User.Identity.GetUserId();
                    odmUnitOfWork.IngredientsRepository.Insert(ingredientVM.Ingredient);
                }
                else
                {
                    odmUnitOfWork.IngredientsRepository.Update(ingredientVM.Ingredient);
                }

                odmUnitOfWork.Save();
                TempData["message"] = string.Format(
                    "{0} has been saved", ingredientVM.Ingredient.Name);

                if (ingredientVM.ReturnUrl != null)
                {
                    return Redirect(ingredientVM.ReturnUrl);
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                return View(ingredientVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id, string returnUrl)
        {
            Ingredient ingredientToDelete = odmUnitOfWork.IngredientsRepository
                                                .GetById(Id);
            
            if (ingredientToDelete != null)
	        {
                odmUnitOfWork.IngredientsRepository.Delete(Id);
                odmUnitOfWork.Save();
                TempData["message"] = string.Format(
                    "{0} has been successfully deleted", ingredientToDelete.Name);
	        }

            return Redirect(returnUrl);
        }
	}
}