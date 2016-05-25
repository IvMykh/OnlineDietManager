using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    public class DaysController : Controller
    {
        private IUnitOfWork OdmUnitOfWork { get; set; }
        //private int? selectedDay { get; set; }


        public DaysController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Index(int courseRefId, int? selectedDayId, string returnUrl) //
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
                ReturnUrl = returnUrl,

                SelectedDayId = selectedDayId //
            });
        }

        [HttpPost]
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

        [HttpGet]
        public ActionResult ChooseDay(int dayId, string returnUrl)
        {
            var uriBuilder = new UriBuilder(returnUrl);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["selectedDayId"] = dayId.ToString();
            uriBuilder.Query = query.ToString();

            return Redirect(uriBuilder.ToString());
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Edit(int dayId, string returnUrl)
        {
            Day day = OdmUnitOfWork.DaysRepository.GetById(dayId);

            return PartialView("_EditDayPartial", new EditDayViewModel {
                Day = day,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public ActionResult Edit(EditDayViewModel editDayVM)
        {
            if (ModelState.IsValid)
            {
                OdmUnitOfWork.DaysRepository.Update(editDayVM.Day);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Day '{0}' has been saved", editDayVM.Day.ID);

                return Redirect(editDayVM.ReturnUrl);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [HttpPost]
        public ActionResult Delete(int dayId, string returnUrl)
        {
            Day dayToDelete = OdmUnitOfWork.DaysRepository
                                    .GetById(dayId);

            if (dayToDelete != null)
            {
                OdmUnitOfWork.DaysRepository.Delete(dayId);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Day '{0}' has been successfully deleted", dayToDelete.ID);
            }

            var uriBuilder = new UriBuilder(returnUrl);
            
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Remove("selectedDayId");
            uriBuilder.Query = query.ToString();
            
            return Redirect(uriBuilder.ToString());
        }
    }
}