using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Controllers;
using System.Web.Mvc;

namespace OnlineDietManager.Tests.CoursesManagement
{
    [TestClass]
    public class ActiveCourseControllerTests
    {
        private const string SAMPLE_USER_ID = "sample id";
        private const string SAMPLE_USER_NAME = "sample user name";
        private const string SAMPLE_COURSE_DESC = "sample course description";
        private const string SAMPLE_RETURN_URL = "return url";

        private const int    SAMPLE_COURSE_ID = 1;

        private Mock<IUnitOfWork>               mockUoW;
        private Mock<IRepository<Course>>       mockCoursesRepo;
        private Mock<IRepository<ActiveCourse>> mockActiveCoursesRepo;
        private ActiveCoursesController         testedController;

        private List<Course> getCourses()
        {
            return new List<Course> {
                    new Course { 
                        ID = SAMPLE_COURSE_ID, 
                        Description = "Course #1", 
                        OwnerID = null,           
                        Days = new List<Day>() 
                    },
                    new Course { 
                        ID = 2, 
                        Description = "Course #2", 
                        OwnerID = null,
                        Days = new List<Day>() 
                    },
                    new Course { 
                        ID = 3, 
                        Description = "Course #3", 
                        OwnerID = SAMPLE_USER_ID, 
                        Days = new List<Day>() 
                    },
                };
        }

        private List<ActiveCourse> getActiveCourses()
        {
            return new List<ActiveCourse> {
                new ActiveCourse { 
                    ID = 1, 
                    Course = mockCoursesRepo.Object.GetAll().First(), 
                    StartDate = DateTime.Now.Date
                }
            };
        }

        private IRepository<Course> createCoursesRepo()
        {
            List<Course> courses = getCourses();

            mockCoursesRepo = new Mock<IRepository<Course>>();
            mockCoursesRepo.Setup(m => m.GetAll())
                .Returns(courses.AsQueryable<Course>());

            mockCoursesRepo
                .Setup(m => m.GetById(It.Is<int>(id => id == courses.First().ID)))
                .Returns(courses.First());

            mockCoursesRepo.Setup(m => m.Update(It.IsAny<Course>()))
                .Verifiable();

            mockCoursesRepo.Setup(m => m.Delete(It.IsAny<int>()))
                .Verifiable();

            mockCoursesRepo.Setup(m => m.Insert(It.IsAny<Course>()))
                .Verifiable();

            return mockCoursesRepo.Object;
        }

        private IRepository<ActiveCourse> createActiveCoursesRepo()
        {
            List<ActiveCourse> activeCourses = getActiveCourses();

            mockActiveCoursesRepo = new Mock<IRepository<ActiveCourse>>();

            mockActiveCoursesRepo.Setup(m => m.GetAll())
                .Returns(activeCourses.AsQueryable<ActiveCourse>());

            mockActiveCoursesRepo.Setup(m => m.Delete(It.Is<int>(argId => argId == SAMPLE_COURSE_ID)))
                .Verifiable();

            mockActiveCoursesRepo.Setup(m => m.Insert(It.IsAny<ActiveCourse>()))
                .Verifiable();

            return mockActiveCoursesRepo.Object;
        }

        private void setupMockUoW()
        {
            mockUoW = new Mock<IUnitOfWork>();

            mockUoW
                .SetupGet(m => m.CoursesRepository)
                .Returns(createCoursesRepo());

            mockUoW
                .SetupGet(m => m.ActiveCoursesRepository)
                .Returns(createActiveCoursesRepo());
        }

        private void setupController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_USER_NAME);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            testedController = new ActiveCoursesController(mockUoW.Object);
            testedController.ControllerContext = controllerContext.Object;
        }

        [TestInitialize]
        public void initData()
        {
            setupMockUoW();
            setupController();
        }




        [TestMethod]
        public void TestPlayCourse()
        {
            // arrange.
            var controller       = testedController;
            int courseToLaunchId = mockUoW.Object.CoursesRepository.GetAll().First().ID;

            // act.
            var actionRes = testedController.Launch(courseToLaunchId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, actionRes.Url);
            mockActiveCoursesRepo.Verify(m => m.Insert(It.IsAny<ActiveCourse>()));
        }

        [TestMethod]
        public void TestStopCourse()
        {
            // arrange.
            var controller = testedController;
            int courseToStopId = mockUoW.Object.ActiveCoursesRepository.GetAll().First().ID;

            // act.
            var actionRes = testedController.StopCourse(SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, actionRes.Url);
            mockActiveCoursesRepo.Verify(m => m.Delete(It.Is<int>(argId => argId == SAMPLE_COURSE_ID)));
        }
    }
}
