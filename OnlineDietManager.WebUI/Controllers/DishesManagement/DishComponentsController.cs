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
        private IUnitOfWork OdmUnitOfWork { get; set; }

        public DishComponentsController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        [ChildActionOnly]
        public ActionResult Index(int dishRefId, string returnUrl)
        {
            IEnumerable<DishComponent> components = 
                OdmUnitOfWork.DishComponentsRepository
                    .GetAll()
                    .Where(dc => dc.DishRefID == dishRefId)
                    .OrderBy(dc => dc.Ingredient.Name)
                    .ToList();

            Dish referedDish = OdmUnitOfWork.DishesRepository.GetById(dishRefId);
            OwnerPolicy ownerPolicy = referedDish.OwnerID == null ?
                                        OwnerPolicy.GlobalOnly :
                                        OwnerPolicy.UserOnly;

            return PartialView("_ListDishComponentsPartial", new ListDishComponentsViewModel 
                {
                    DishComponents = components,
                    DishRefId = dishRefId,
                    ReturnUrl = returnUrl,
                    OwnerPolicy = ownerPolicy
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
        public ActionResult Create(string returnUrl, int dishRefId, OwnerPolicy ownerPolicy)
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<Ingredient> model = null;

            switch (ownerPolicy)
            {
                case OwnerPolicy.UserOnly:
                    {
                        model = OdmUnitOfWork.IngredientsRepository
                                .GetAll()
                                .Where(ing => ing.OwnerID == userId);
                    } break;
                case OwnerPolicy.GlobalOnly:
                    {
                        model = OdmUnitOfWork.IngredientsRepository
                                .GetAll()
                                .Where(ing => ing.OwnerID == null);
                    } break;
                case OwnerPolicy.Both:
                    {
                        model = OdmUnitOfWork.IngredientsRepository
                                .GetAll()
                                .Where(ing => ing.OwnerID == userId || ing.OwnerID == null);
                    } break;

                default: throw new ArgumentException(
                    string.Format("'{0}': unexpected owner policy type"));
            }

            return PartialView("_CreateDishComponentPartial",
                model.OrderBy(ing => ing.Name)
                     .Select(ing => new SelectIngredientViewModel
                        {
                            Ingredient = ing,
                            DishRefId = dishRefId,
                            Weight = SpecialData.DEFAULT_COMPONENT_WEIGHT,
                            ReturnUrl = returnUrl
                        }));
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
                        Ingredient = OdmUnitOfWork.IngredientsRepository
                                        .GetById(dishComponentVM.Id)
                    };

                var componentEntry = OdmUnitOfWork.DishComponentsRepository
                                        .GetById(dishComponentVM.Id, dishComponentVM.DishRefId);
                
                if (componentEntry != null)
                {
                    componentEntry.Weight += dishComponentVM.Weight;
                }
                else
                {
                    OdmUnitOfWork.DishComponentsRepository.Insert(newDishComponent);
                }
                OdmUnitOfWork.Save();

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
            DishComponent component = OdmUnitOfWork.DishComponentsRepository
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
                OdmUnitOfWork.DishComponentsRepository.Update(componentVM.DishComponent);
                OdmUnitOfWork.Save();

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
            DishComponent componentToDelete = OdmUnitOfWork.DishComponentsRepository
                                                .GetById(ingId, dishRefId);

            string ingredientName = OdmUnitOfWork.IngredientsRepository
                                    .GetById(ingId)
                                    .Name;

            if (componentToDelete != null)
            {
                OdmUnitOfWork.DishComponentsRepository.Delete(ingId, dishRefId);
                OdmUnitOfWork.Save();
                TempData["message"] = string.Format(
                    "{0} has been successfully deleted", ingredientName);
            }

            return Redirect(returnUrl);
                // "Edit", "Dishes", new { Id = componentToDelete.DishRefID }
        }
	}
}