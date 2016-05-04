using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class CoursesController : Controller
    {
        private IUnitOfWork UnitOfWork { get; set; }

        public CoursesController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        // GET: Courses
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var res = new List<Course>();
            var model = UnitOfWork.CoursesRepository.
                GetAll().
                Where(course => course.OwnerID == user);
            res.AddRange(model);

            model = UnitOfWork.CoursesRepository.
                GetAll().
                Where(course => course.OwnerID == null);

            res.AddRange(model);

            ViewBag.isAdmin = User.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin));
            ViewBag.UserId = user;

            return View("Index", res);
        }
    }
}