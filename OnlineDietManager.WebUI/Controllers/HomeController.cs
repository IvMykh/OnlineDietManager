using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.UnitsOfWork;

namespace OnlineDietManager.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork uow;

        public HomeController(IUnitOfWork uowParam)
        {
            uow = uowParam;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(uow.IngredientsRepository.GetAll().ToList());
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