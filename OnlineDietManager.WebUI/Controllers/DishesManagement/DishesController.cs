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
    public class DishesController 
        : AbstrDishesController
    {
        public DishesController(IUnitOfWork unitOfWork)
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
        protected override RedirectToRouteResult GetRedirectToActionFor(string actionName, object routeParams = null)
        {
            if (routeParams == null)
            {
                return RedirectToAction(actionName);
            }

            return RedirectToAction(actionName, routeParams);
        }

        protected override object GetIndexModel()
        {
            string userId = User.Identity.GetUserId();

            var model = OdmUnitOfWork.DishesRepository.GetAll()
                            .Where(dish => dish.OwnerID == userId)
                            .OrderBy(dish => dish.Name)
                            .ToList<Dish>();   

            return model;
        }
        protected override string GetOwnerName()
        {
            return User.Identity.GetUserId();
        }

        [HttpGet]
        public new ActionResult Create(string returnUrl)
        {
            return base.Create(returnUrl);
        }

        [HttpPost]
        public new ActionResult Create(DishViewModel newDishVM)
        {
            return base.Create(newDishVM);
        }

        [HttpGet]
        public new ActionResult Edit(int Id, string returnUrl)
        {
            return base.Edit(Id, returnUrl);
        }

        [HttpPost]
        public new ActionResult Edit(DishViewModel dishVM)
        {
            return base.Edit(dishVM);
        }

        [HttpPost]
        public new ActionResult Delete(int Id, string returnUrl)
        {
            return base.Delete(Id, returnUrl);
        }
	}
}