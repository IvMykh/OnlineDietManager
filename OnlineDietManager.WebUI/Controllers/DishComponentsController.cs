using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    public class DishComponentsController : Controller
    {
        private IUnitOfWork odmUnitOfWork;

        public DishComponentsController(IUnitOfWork uowParam)
        {
            odmUnitOfWork = uowParam;
        }

        [ChildActionOnly]
        public ActionResult Index(int dishRefId, string returnUrl)
        {
            IEnumerable<DishComponent> components = odmUnitOfWork.DishComponentsRepository
                                                    .GetAll()
                                                    .Where(dc => dc.DishRefID == dishRefId)
                                                    .OrderBy(dc => dc.Ingredient.Name)
                                                    .ToList();

            return PartialView("_ListDishComponentsPartial", new ListDishComponentsViewModel 
                {
                    DishComponents = components,
                    DishRefId = dishRefId,
                    ReturnUrl = returnUrl
                });
        }

        //[HttpGet]
        //[ChildActionOnly]
        //public ActionResult Create(string returnUrl, int dishRefId)
        //{
        //    string userId = User.Identity.GetUserId();
        //
        //    IEnumerable<Ingredient> sortedIngredients = 
        //        odmUnitOfWork.IngredientsRepository
        //            .GetAll()
        //            .Where(ing => ing.OwnerID == userId)
        //            .OrderBy(ing => ing.Name);
        //
        //    ViewBag.ID = new SelectList(
        //        items:          sortedIngredients, 
        //        dataValueField: "Id", 
        //        dataTextField:  "Name"
        //        );
        //    
        //    return PartialView("_CreateDishComponentPartial", 
        //        new CreateDishComponentViewModel 
        //            {
        //                DishRefId = dishRefId,
        //                ReturnUrl = returnUrl
        //            });
        //}

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Create(string returnUrl, int dishRefId)
        {
            string userId = User.Identity.GetUserId();

            IEnumerable<SelectIngredientViewModel> sortedIngredients =
                odmUnitOfWork.IngredientsRepository
                    .GetAll()
                    .Where(ing => ing.OwnerID == userId) //|| local ingredients 
                                  // ing.OwnerID == null)
                    .OrderBy(ing => ing.Name)
                    .Select(ing => new SelectIngredientViewModel
                        {
                            Ingredient = ing,
                            DishRefId = dishRefId,
                            Weight = SpecialData.DEFAULT_COMPONENT_WEIGHT,
                            ReturnUrl = returnUrl
                        });

            return PartialView("_CreateDishComponentPartial", sortedIngredients);
        }

        [HttpPost]
        public ActionResult Create(CreateDishComponentViewModel dishComponentVM)
        {
            if (ModelState.IsValid)
            {
                var newDishComponent = new DishComponent
                    {
                        ID = dishComponentVM.Id,
                        DishRefID = dishComponentVM.DishRefId,
                        Weight = dishComponentVM.Weight,
                        Ingredient = odmUnitOfWork.IngredientsRepository
                                        .GetById(dishComponentVM.Id)
                    };

                var componentEntry = odmUnitOfWork.DishComponentsRepository
                                        .GetById(dishComponentVM.Id, dishComponentVM.DishRefId);
                
                if (componentEntry != null)
                {
                    componentEntry.Weight += dishComponentVM.Weight;
                }
                else
                {
                    odmUnitOfWork.DishComponentsRepository.Insert(newDishComponent);
                }
                odmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} component has been saved", newDishComponent.Ingredient.Name);
            }
            else
            {
                var errorMessages = new List<string>();

                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }

                TempData["errorMessage"] =
                    errorMessages.Count != 0 ? errorMessages : null; // "Specified component cannot be added as it is invalid";
            }
            
            return Redirect(dishComponentVM.ReturnUrl);
        }

        [HttpGet]
        public ActionResult Edit(int ID, int dishRefId, string returnUrl)
        {
            DishComponent component = odmUnitOfWork.DishComponentsRepository
                                        .GetById(ID, dishRefId);
            return View(new DishComponentViewModel
                {
                    DishComponent = component,
                    ReturnUrl = returnUrl
                });
        }

        [HttpPost]
        public ActionResult Edit(DishComponentViewModel componentVM)
        {
            if (ModelState.IsValid)
            {
                odmUnitOfWork.DishComponentsRepository.Update(componentVM.DishComponent);
                odmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} component has been saved", componentVM.DishComponent.Ingredient.Name);

                return Redirect(componentVM.ReturnUrl);
                    //"Edit", "Dishes", new { Id = componentViewModel.DishComponent.DishRefID }
            }
            else
            {
                return View(componentVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int ingId, int dishRefId, string returnUrl)
        {
            DishComponent componentToDelete = odmUnitOfWork.DishComponentsRepository
                                                .GetById(ingId, dishRefId);

            string ingredientName = odmUnitOfWork.IngredientsRepository
                                    .GetById(ingId)
                                    .Name;

            if (componentToDelete != null)
            {
                odmUnitOfWork.DishComponentsRepository.Delete(ingId, dishRefId);
                odmUnitOfWork.Save();
                TempData["message"] = string.Format(
                    "{0} has been successfully deleted", ingredientName);
            }

            return Redirect(returnUrl);
                // "Edit", "Dishes", new { Id = componentToDelete.DishRefID }
        }
	}
}