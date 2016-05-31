using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.UnitsOfWork;

namespace OnlineDietManager.WebUI.Controllers
{
    public class ActiveCoursesController : Controller
    {
        private IUnitOfWork odmUnitOfWork { get; set; }
        
        public ActiveCoursesController(IUnitOfWork uow)
        {
            odmUnitOfWork = uow;
        }

        // GET: ActiveCourses
        public ActionResult Index()
        {
            return View();
        }

        private void stopPlayingCourseIfAny()
        {
            var owner = User.Identity.GetUserId();

            var presentActive = odmUnitOfWork.ActiveCoursesRepository.GetAll()
                                    .Where(course => course.Course.OwnerID == owner)
                                    .FirstOrDefault();

            if (presentActive != null)
            {
                odmUnitOfWork.ActiveCoursesRepository.Delete(presentActive.ID);
                odmUnitOfWork.Save();
            }
        }

        [HttpPost]
        public ActionResult StopCourse(string returnUrl)
        {
            stopPlayingCourseIfAny();
            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult Launch(int idToLaunch, string returnUrl)
        {
            var courseToLaunch = odmUnitOfWork.CoursesRepository.GetById(idToLaunch);
            var owner = User.Identity.GetUserId();

            stopPlayingCourseIfAny();

            Course finalCourseToLaunch = null;

            if (courseToLaunch.OwnerID != null)
            {
                finalCourseToLaunch = courseToLaunch;
            }
            else
            {
                finalCourseToLaunch = EntityCopyer.Instance.CopyCourse(courseToLaunch, owner, odmUnitOfWork);
            }

            odmUnitOfWork.ActiveCoursesRepository.Insert(new ActiveCourse() {
                Course = finalCourseToLaunch,
                StartDate = DateTime.Now.Date
            });
            odmUnitOfWork.Save();

            TempData["message"] = string.Format(
                    "{0} has been successfully launched", idToLaunch);

            return Redirect(returnUrl);
        }
    }
}