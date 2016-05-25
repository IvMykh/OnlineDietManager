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
    public abstract class AbstrCoursesController : Controller
    {
        protected IUnitOfWork OdmUnitOfWork { get; private set; }

        public AbstrCoursesController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }

        protected abstract ViewResult GetViewResultFor(
            string viewName = null, 
            object model = null);

        protected abstract RedirectToRouteResult GetRedirectToActionFor(
            string actionName, 
            object routeParams = null);

        protected abstract object GetIndexModel();
        protected abstract string GetOwnerName();

        public ActionResult Index()
        {
            return GetViewResultFor("Index", GetIndexModel());
        }

        // GET.
        public ActionResult Create(string returnUrl)
        {
            var model = new CourseViewModel {
                Course = new Course(),
                //Index = 0,
                ReturnUrl = returnUrl
            };

            return GetViewResultFor("Create", model);
        }

        // POST.
        public ActionResult Create(CourseViewModel newCourseVM)
        {
            if (ModelState.IsValid)
            {
                string userId = GetOwnerName();
                newCourseVM.Course.OwnerID = userId;

                OdmUnitOfWork.CoursesRepository.Insert(newCourseVM.Course);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Course '{0}' has been saved", newCourseVM.Course.ID);

                return GetRedirectToActionFor("Edit", new
                {
                    Id = newCourseVM.Course.ID,
                    returnUrl = newCourseVM.ReturnUrl
                });
            }
            else
            {
                return GetViewResultFor("Create", newCourseVM);
            }
        }

        // GET.
        public ActionResult Edit(int Id, int? selectedDayId, string returnUrl)
        {
            var courseToEdit = OdmUnitOfWork.CoursesRepository
                                .GetAll()
                                .FirstOrDefault(course => course.ID == Id);

            return GetViewResultFor("Edit", new CourseViewModel
            {
                Course = courseToEdit,
                SelectedDayId = selectedDayId,
                ReturnUrl = returnUrl
            });
        }

        // POST.
        public ActionResult Edit(CourseViewModel courseVM)
        {
            if (ModelState.IsValid)
            {
                OdmUnitOfWork.CoursesRepository.Update(courseVM.Course);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Course '{0}' has been saved", courseVM.Course.ID);

                if (courseVM.ReturnUrl != null)
                {
                    return Redirect(courseVM.ReturnUrl);
                }

                return GetRedirectToActionFor("Index");
            }
            else
            {
                return GetViewResultFor("Edit", courseVM);
            }
        }

        // POST.
        public ActionResult Delete(int Id, string returnUrl)
        {
            Course courseToDelete = OdmUnitOfWork.CoursesRepository
                                    .GetById(Id);

            if (courseToDelete != null)
            {
                OdmUnitOfWork.CoursesRepository.Delete(Id);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Course '{0}' has been successfully deleted", courseToDelete.ID);
            }

            return Redirect(returnUrl);
        }
	}
}