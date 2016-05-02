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
    public class GeneralDishesController 
        : AbstrDishesController
    {
        public GeneralDishesController(IUnitOfWork unitOfWork)
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
            var model = OdmUnitOfWork.DishesRepository.GetAll()
                            .Where(dish => dish.OwnerID == null)
                            .OrderBy(dish => dish.Name)
                            .ToList<Dish>();

            return model;
        }
        protected override string GetOwnerName()
        {
            return null;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public new ActionResult Create(string returnUrl)
        {
            return base.Create(returnUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Create(DishViewModel newDishVM)
        {
            return base.Create(newDishVM);
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public new ActionResult Edit(int Id, string returnUrl)
        {
            return base.Edit(Id, returnUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Edit(DishViewModel dishVM)
        {
            return base.Edit(dishVM);
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
            // TODO: implement;
            return null;
        }
	}
}