using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class DaysController : Controller
    {
        // GET: Days
        private IUnitOfWork OdmUnitOfWork { get; set; }

        public DaysController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        [ChildActionOnly]
        public ActionResult Index(int courseRefId, string returnUrl)
        {
            IEnumerable<Day> days =
                OdmUnitOfWork.DaysRepository
                    .GetAll()
                    .Where(dc => dc.CourseID== courseRefId)
                    .ToList();

            Day referedDay = OdmUnitOfWork.DaysRepository.GetById(courseRefId);

            return PartialView("_DayPartial", new ListDayViewModel
            {
                Days = days,
                CourseRefId = courseRefId,
                ReturnUrl = returnUrl
            });
        }

        //[HttpGet]
        //[ChildActionOnly]
        //public ActionResult Create(string returnUrl, int CourseRefId)
        //{

                
        //}

        //[HttpPost]
        //[ChildActionOnly]
        //public ActionResult Create()
    }
}