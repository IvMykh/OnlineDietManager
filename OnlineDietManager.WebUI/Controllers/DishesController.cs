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
	}
}