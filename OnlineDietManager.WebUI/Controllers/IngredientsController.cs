using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;

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
        public ActionResult Create()
        {
            return View("Edit", new Ingredient());
        }

        // ...

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            return View(odmUnitOfWork.IngredientsRepository.GetAll()
                .Where(ing => ing.OwnerID == userId)
                .ToList<Ingredient>());
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var ingredientToEdit = odmUnitOfWork.IngredientsRepository.GetAll()
                .FirstOrDefault(ing => ing.ID == Id);
            
            return View(ingredientToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ingredient.OwnerID))
                {
                    ingredient.OwnerID = User.Identity.GetUserId();
                    odmUnitOfWork.IngredientsRepository.Insert(ingredient);
                }
                else
                {
                    odmUnitOfWork.IngredientsRepository.Update(ingredient);
                }

                odmUnitOfWork.Save();

                TempData["message"] = string.Format("{0} has been saved", ingredient.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(ingredient);
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Ingredient ingredientToDelete = odmUnitOfWork.IngredientsRepository.GetById(Id);
            if (ingredientToDelete != null)
	        {
                odmUnitOfWork.IngredientsRepository.Delete(Id);
                odmUnitOfWork.Save();
                TempData["message"] = string.Format("{0} has been successfully deleted", ingredientToDelete.Name);
	        }

            return RedirectToAction("Index");
        }
	}
}