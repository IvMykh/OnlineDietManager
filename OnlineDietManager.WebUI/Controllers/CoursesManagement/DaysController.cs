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
        private IUnitOfWork OdmUnitOfWork { get; set; }
        private int? selectedDay { get; set; }


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
                    .Where(dc => dc.CourseID == courseRefId)
                    .ToList();

            Day referedDay = OdmUnitOfWork.DaysRepository.GetById(courseRefId);

            return PartialView("_ListDaysPartial", new ListDaysViewModel
            {
                CourseRefId = courseRefId,
                Days = days,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        //[ChildActionOnly]
        public ActionResult Create(AddDayViewModel addDayVM)
        {
            var newDay = new Day { 
                    CourseID = addDayVM.CourseId 
                };

            OdmUnitOfWork.DaysRepository.Insert(newDay);
            OdmUnitOfWork.Save();

            var uriBuilder = new UriBuilder(addDayVM.ReturnUrl);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["selectedDayId"] = newDay.ID.ToString();
            uriBuilder.Query = query.ToString();

            return Redirect(uriBuilder.ToString());
        }
    }
}