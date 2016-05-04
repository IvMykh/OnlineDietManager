using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;

namespace OnlineDietManager.WebUI.Controllers
{
    public class HomeController : AppController
    {
        private IUnitOfWork OdmUnitOfWork { get; set; }

        public HomeController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
	}
}