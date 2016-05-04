using OnlineDietManager.Domain.CoursesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class ActiveCoursesController : Controller
    {
        // GET: ActiveCourses
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Launch()
        {
            var coursetoLaunch = new ActiveCourse
            {
                
            }
            return View();
        }
    }
}