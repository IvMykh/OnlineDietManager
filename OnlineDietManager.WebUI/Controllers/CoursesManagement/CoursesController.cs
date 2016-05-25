using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDietManager.WebUI.Controllers
{
    public class CoursesController
        : AbstrCoursesController
    {
        public CoursesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        //
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

        protected override object GetIndexModel()
        {
            string userId = User.Identity.GetUserId();

            var model = OdmUnitOfWork.CoursesRepository.GetAll()
                            .Where(course => course.OwnerID == userId)
                            .OrderBy(course => course.ID)
                            .ToList<Course>();

            return model;
        }
        protected override string GetOwnerName()
        {
            return User.Identity.GetUserId();
        }

        [HttpGet]
        public new ActionResult Create(string returnUrl)
        {
            return base.Create(returnUrl);
        }

        [HttpPost]
        public new ActionResult Create(CourseViewModel newDishVM)
        {
            return base.Create(newDishVM);
        }


        [HttpGet]
        public new ActionResult Edit(int Id, int? selectedDayId, string returnUrl)
        {
            return base.Edit(Id, selectedDayId, returnUrl);
        }

        [HttpPost]
        public new ActionResult Edit(CourseViewModel courseVM)
        {
            return base.Edit(courseVM);
        }
    }

    //public class CoursesController : Controller
    //{
    //    private Course newCourse = null;
    //    private IUnitOfWork UnitOfWork { get; set; }

    //    public CoursesController(IUnitOfWork uow)
    //    {
    //        UnitOfWork = uow;
    //    }
    //    // GET: Courses
    //    public ActionResult Index()
    //    {
    //        string user = User.Identity.GetUserId();
    //        var res = new List<Course>();
    //        var model = UnitOfWork.CoursesRepository.
    //            GetAll().
    //            Where(course => course.OwnerID == user);
    //        res.AddRange(model);

    //        model = UnitOfWork.CoursesRepository.
    //            GetAll().
    //            Where(course => course.OwnerID == null);

    //        res.AddRange(model);

    //        ViewBag.isAdmin = User.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin));
    //        ViewBag.UserId = user;

    //        return View("Index", res);
    //    }

    //    [HttpPost]
    //    public ActionResult AddToPersonal(int Id, string returnUrl)
    //    {
    //        Course courseToAdd = UnitOfWork.CoursesRepository.GetById(Id);

    //        if (courseToAdd != null)
    //        {
    //            Course coursePersonalCopy = new Course
    //            {
    //                Days = new List<Day>(),
    //                Description = courseToAdd.Description,
    //                OwnerID = User.Identity.GetUserId()
    //            };


    //            foreach (var day in courseToAdd.Days)
    //            {
    //                var dayCopy = new Day
    //                {
    //                    Description = day.Description,
    //                    Meals = new List<Meal>()
    //                };

    //                foreach(var meal in day.Meals)
    //                {
    //                    var mealCopy = new Meal
    //                    {
    //                        Description = meal.Description,
    //                        Time = meal.Time,
    //                        Dishes = new List<Dish>()
    //                    };

    //                    foreach(var dish in meal.Dishes)
    //                    {
    //                        mealCopy.Dishes.Add(dish);
    //                    }

    //                    dayCopy.Meals.Add(mealCopy);
    //                }

    //                coursePersonalCopy.Days.Add(dayCopy);
    //            }

    //            UnitOfWork.CoursesRepository.Insert(coursePersonalCopy);
    //            UnitOfWork.Save();

    //            TempData["message"] = string.Format(
    //                "{0} has been successfully added to personal courses", courseToAdd.ID);
    //        }

    //        return Redirect(returnUrl);
    //    }

    //    [HttpGet]
    //    public ActionResult Create(string returnUrl)
    //    {
    //        //var course = new Course();
    //        //course.Days = new List<Day>();
    //        //var model = new CourseViewModel
    //        //{
    //        //    Course = course,
    //        //    ReturnUrl = returnUrl
    //        //};

    //        newCourse = new Course();
    //        newCourse.Days = new List<Day>();

    //        var model = new CourseViewModel
    //        {
    //            Course = newCourse,
    //            ReturnUrl = returnUrl
    //        };
    //        return View("Edit", model);
    //    }

    //    //[HttpPost]
    //    //public ActionResult Create(CourseViewModel vm)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        string userId = User.Identity.GetUserId();
    //    //        vm.Course.OwnerID = userId;

    //    //        UnitOfWork.CoursesRepository.Insert(vm.Course);
    //    //        UnitOfWork.Save();

    //    //        TempData["message"] = string.Format(
    //    //            "{0} has been saved", vm.Course.ID);

    //    //        return RedirectToAction("Edit", new
    //    //        {
    //    //            Id = vm.Course.ID,
    //    //            returnUrl = vm.ReturnUrl
    //    //        });
    //    //    }
    //    //    else
    //    //    {
    //    //        return View("Edit", vm);
    //    //    }
    //    //}

    //    [HttpGet]
    //    public ActionResult Edit(int id, string returnUrl)
    //    {
    //        var courseToEdit = UnitOfWork.CoursesRepository.
    //            GetAll().FirstOrDefault(cour => cour.ID == id);

    //        return View(new CourseViewModel
    //        {
    //            Course = courseToEdit,
    //            ReturnUrl = returnUrl
    //        });
    //    }

    //    [HttpPost]
    //    public ActionResult Edit(CourseViewModel vm)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            if(vm.Course.ID == 0)
    //            {
    //                vm.Course.OwnerID = User.Identity.GetUserId();
    //                UnitOfWork.CoursesRepository.Insert(vm.Course);
    //            }
    //            else
    //            {
    //                UnitOfWork.CoursesRepository.Update(vm.Course);
    //            }

    //            UnitOfWork.Save();
    //            TempData["message"] = string.Format(
    //                "{0} has been saved", vm.Course.ID);

    //            if (vm.ReturnUrl != null)
    //            {
    //                return Redirect(vm.ReturnUrl);
    //            }

    //            return RedirectToAction("Index");
    //        }
    //        else
    //        {
    //            return View(vm);
    //        }
        
    //    }

    //    [HttpGet]
    //    public ActionResult ChooseDay(CourseViewWithDayIdModel m)
    //    {
    //        m.CVM.Index = m.index;

    //        return View("Edit", m.CVM);
    //    }
        
    //    [HttpPost]
    //    public ActionResult AddDay(CourseViewModel cvm)
    //    {
    //        //cvm.Course.Days.Add(new Day());

    //        //Course c = UnitOfWork.CoursesRepository.GetById(cvm.Course.ID);
    //        //c.Days.Add(new Day());

    //        //UnitOfWork.CoursesRepository.Update(cvm.Course);
    //        //UnitOfWork.Save();


    //        newCourse.Days.Add(new Day());

    //        return View("Edit", cvm);
    //    }
    //}
}