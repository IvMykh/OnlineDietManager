using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Models;
using Microsoft.AspNet.Identity;

namespace OnlineDietManager.WebUI.Controllers
{
    public class GeneralCoursesController 
        : AbstrCoursesController
    {
         public GeneralCoursesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override ViewResult GetViewResultFor(string viewName = null, object model = null)
        {
            if (viewName == null && model == null)
                return View();

            if (viewName == null)
                return View(model);

            if (model == null)
                return View(viewName);

            return View(viewName, model);
        }
        protected override RedirectToRouteResult GetRedirectToActionFor(string actionName, object routeParams = null)
        {
            if (routeParams == null)
            {
                return RedirectToAction(actionName);
            }

            return RedirectToAction(actionName, routeParams);
        }
        protected override string GetOwnerName()
        {
            return null;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public new ActionResult Create(string returnUrl)
        {
            return base.Create(returnUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Create(CourseViewModel newDishVM)
        {
            return base.Create(newDishVM);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public new ActionResult Edit(int Id, int? selectedDayId, string returnUrl)
        {
            return base.Edit(Id, selectedDayId, returnUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Edit(CourseViewModel courseVM)
        {
            return base.Edit(courseVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public new ActionResult Delete(int Id, string returnUrl)
        {
            return base.Delete(Id, returnUrl);
        }

        [HttpPost]
        public ActionResult AddToPersonal(int Id, string returnUrl)
        {
            Course courseToAdd = OdmUnitOfWork.CoursesRepository.GetById(Id);

            if (courseToAdd != null)
            {
                Course coursePersonalCopy = 
                    EntityCopyer.Instance.CopyCourse(courseToAdd, User.Identity.GetUserId(), OdmUnitOfWork);

                OdmUnitOfWork.CoursesRepository.Insert(coursePersonalCopy);
                OdmUnitOfWork.Save();

                TempData["message"] = string.Format(
                    "Course '{0}' has been successfully added to personal courses", courseToAdd.ID);
            }

            return Redirect(returnUrl);
        }
    }
}