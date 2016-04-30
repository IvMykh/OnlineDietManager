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
    public class DishesController : Controller
    {
        private IUnitOfWork odmUnitOfWork;

        public DishesController(IUnitOfWork unitOfWork)
        {
            odmUnitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            return View(odmUnitOfWork.DishesRepository.GetAll()
                        .Where(ing => ing.OwnerID == userId)
                        .ToList<Dish>());
        }

        [HttpGet]
        public ActionResult Edit(int Id, string returnUrl)
        {
            string userId = User.Identity.GetUserId();

            var dishToEdit = odmUnitOfWork.DishesRepository
                            .GetAll()
                            .Where(dish => dish.OwnerID == userId)
                            .FirstOrDefault(dish => dish.ID == Id);

            return View(new DishViewModel 
                { 
                    Dish = dishToEdit, 
                    ReturnUrl = returnUrl 
                });
        }

        [HttpPost]
        public ActionResult Edit(DishViewModel dishVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(dishVM.Dish.OwnerID))
                {
                    dishVM.Dish.OwnerID = User.Identity.GetUserId();
                    odmUnitOfWork.DishesRepository.Insert(dishVM.Dish);
                }
                else
                {
                    odmUnitOfWork.DishesRepository.Update(dishVM.Dish);
                }
                odmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "{0} has been saved", dishVM.Dish.Name);

                if (dishVM.ReturnUrl != null)
                {
                    return Redirect(dishVM.ReturnUrl);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(dishVM);
            }
        }
	}
}