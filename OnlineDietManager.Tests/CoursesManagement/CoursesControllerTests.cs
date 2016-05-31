using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Controllers;
using System.Collections;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.Tests.CoursesManagement
{
    [TestClass]
    public class CoursesControllerTests
    {
        private const string SAMPLE_USER_ID = "sample id";
        private const string SAMPLE_USER_NAME = "sample user name";
        private const string SAMPLE_COURSE_DESC = "sample course description";
        private const string SAMPLE_RETURN_URL = "return url";

        private Mock<IUnitOfWork>           mockUoW;
        private Mock<IRepository<Course>>   mockCoursesRepo;
        private GeneralCoursesController    testedController;

        private List<Course> getCourses()
        {
            return new List<Course> {
                    new Course { ID = 1, Description = "Course #1", OwnerID = null,           Days = new List<Day>() },
                    new Course { ID = 2, Description = "Course #2", OwnerID = null,           Days = new List<Day>() },
                    new Course { ID = 3, Description = "Course #3", OwnerID = SAMPLE_USER_ID, Days = new List<Day>() },
                };
        }

        private IRepository<Course> createCoursesRepo()
        {
            List<Course> courses = getCourses();

            mockCoursesRepo = new Mock<IRepository<Course>>();
            mockCoursesRepo.Setup(m => m.GetAll())
                .Returns(courses.AsQueryable<Course>());

            // ...
            mockCoursesRepo
                .Setup(m => m.GetById(It.Is<int>(id => id == courses.First().ID)))
                .Returns(courses.First());

            mockCoursesRepo.Setup(m => m.Update(It.IsAny<Course>()))
                .Verifiable();

            mockCoursesRepo.Setup(m => m.Delete(It.IsAny<int>()))
                .Verifiable();

            mockCoursesRepo.Setup(m => m.Insert(It.IsAny<Course>()))
                .Verifiable();
            // .../

            return mockCoursesRepo.Object;
        }

        private void setupMockUoW()
        {
            mockUoW = new Mock<IUnitOfWork>();

            mockUoW
                .SetupGet(m => m.CoursesRepository)
                .Returns(createCoursesRepo());
        }

        private void setupController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_USER_NAME);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            testedController = new GeneralCoursesController(mockUoW.Object);
            testedController.ControllerContext = controllerContext.Object;
        }

        [TestInitialize]
        public void initData()
        {
            setupMockUoW();
            setupController();
        }



        [TestMethod]
        public void TestCoursesIndex()
        {
            // arrange.
            var controller = testedController;

            // act.
            var actionRes = controller.Index() as ViewResult;
            var expectedModel = mockUoW.Object.CoursesRepository
                                    .GetAll()
                                    .Where(c => c.OwnerID == null)
                                    .ToList<Course>();

            // assert.
            CollectionAssert.AreEquivalent(expectedModel as ICollection, actionRes.Model as ICollection, "Message");
        }

        [TestMethod]
        public void TestCoursesCreate()
        {
            // arrange.
            var controller = testedController;

            // act.
            var actionRes = controller.Create(SAMPLE_RETURN_URL) as ViewResult;
            var model = actionRes.Model as CourseViewModel;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, model.ReturnUrl);
            Assert.AreEqual(0, model.Course.ID);
            Assert.AreEqual("Create", actionRes.ViewName);
        }

        [TestMethod]
        public void TestCoursesGetEdit()
        {
            // arrange.
            var controller = testedController;
            Course courseToEdit = mockUoW.Object.CoursesRepository.GetAll().First();

            // act.
            var actionRes = controller.Edit(courseToEdit.ID, null, SAMPLE_RETURN_URL) as ViewResult;
            var model = actionRes.Model as CourseViewModel;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, model.ReturnUrl);
            Assert.AreEqual(courseToEdit.ID, model.Course.ID);
        }

        [TestMethod]
        public void TestCoursesPostEdit()
        {
            // arrange.
            var controller = testedController;
            int courseToEditId = mockUoW.Object.CoursesRepository.GetAll().First().ID;
            string newDesc = SAMPLE_COURSE_DESC;

            Course courseToEdit = mockUoW.Object.CoursesRepository
                                    .GetAll()
                                    .Where(c => c.ID == courseToEditId)
                                    .First();

            CourseViewModel cvm = new CourseViewModel()
            {
                Course = new Course
                {
                    ID = courseToEdit.ID,
                    Description = newDesc,
                    OwnerID = courseToEdit.OwnerID
                },
                ReturnUrl = SAMPLE_RETURN_URL
            };


            // act.
            controller.Edit(cvm);

            // assert.
            mockCoursesRepo.Verify(m => m.Update(It.IsAny<Course>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestCoursesDelete()
        {
            // arrange.
            var controller = testedController;
            int courseToDelId = mockUoW.Object.CoursesRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.Delete(courseToDelId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, actionRes.Url);
            mockCoursesRepo.Verify(m => m.Delete(It.IsAny<int>()));
        }

        [TestMethod]
        public void TestCoursesAddToPersonal()
        {
            // arrange.
            var controller = testedController;
            int courseToAddId = mockUoW.Object.CoursesRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.AddToPersonal(courseToAddId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(actionRes.Url, SAMPLE_RETURN_URL);
            mockCoursesRepo.Verify(m => m.Insert(It.IsAny<Course>()));
        }
    }
}
