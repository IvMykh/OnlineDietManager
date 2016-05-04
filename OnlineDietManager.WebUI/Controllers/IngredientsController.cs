﻿using System;
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
    public class IngredientsController 
        : AbstrIngredientsController 
    {
        public IngredientsController(IUnitOfWork unitOfWork)
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
            string userId = User.Identity.GetUserId();
            
            IEnumerable<Ingredient> model =
                OdmUnitOfWork.IngredientsRepository
                    .GetAll()
                    .Where(ing => ing.OwnerID == userId)
                    .OrderBy(ing => ing.Name)
                    .ToList<Ingredient>();

            return model;
        }
        protected override string GetUserNameForEdit()
        {
            return User.Identity.GetUserId();
        }


        [HttpGet]
        public new ActionResult Create(string returnUrl)
        {
            return base.Create(returnUrl);
        }

        [HttpGet]
        public new ActionResult Edit(int Id, string returnUrl)
        {
            return base.Edit(Id, returnUrl);
        }

        [HttpPost]
        public new ActionResult Edit(IngredientViewModel ingredientVM)
        {
            return base.Edit(ingredientVM);
        }

        [HttpPost]
        public new ActionResult Delete(int Id, string returnUrl)
        {
            return base.Delete(Id, returnUrl);
        }
	}
}