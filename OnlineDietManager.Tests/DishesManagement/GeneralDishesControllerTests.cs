using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Controllers;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.Tests
{
    [TestClass]
    public class GeneralDishesControllerTests
    {
        private const string SAMPLE_USER_ID = "sample id";
        private const string SAMPLE_USER_NAME = "sample user name";
        private const string SAMPLE_DISH_NAME = "sample dish name";
        private const string SAMPLE_RETURN_URL = "return url";

        private Mock<IUnitOfWork>       mockUoW;
        private Mock<IRepository<Dish>> mockDishesRepo;
        private GeneralDishesController testedController;

        private List<Dish> getDishes()
        {
            return new List<Dish> {
                    new Dish { ID = 12,  Name = "Fruit Cake",   Description = "The potato",   OwnerID = null },
                    new Dish { ID = 13,  Name = "Carrot",       Description = "The carrot",   OwnerID = null },
                    new Dish { ID = 14,  Name = "Cucumber",     Description = "The cucumber", OwnerID = SAMPLE_USER_ID },
                };
        }

        private IRepository<Dish> createDishesRepo()
        {
            List<Dish> dishes = getDishes();

            mockDishesRepo = new Mock<IRepository<Dish>>();
            mockDishesRepo.Setup(m => m.GetAll())
                .Returns(dishes.AsQueryable<Dish>());

            mockDishesRepo
                .Setup(m => m.GetById(It.Is<int>(id => id == dishes.First().ID)))
                .Returns(dishes.First());

            mockDishesRepo.Setup(m => m.Update(It.IsAny<Dish>()))
                .Verifiable();

            mockDishesRepo.Setup(m => m.Delete(It.IsAny<int>()))
                .Verifiable();

            mockDishesRepo.Setup(m => m.Insert(It.IsAny<Dish>()))
                .Verifiable();


            return mockDishesRepo.Object;
        }

        private void setupMockUoW()
        {
            mockUoW = new Mock<IUnitOfWork>();

            mockUoW
                .SetupGet(m => m.DishesRepository)
                .Returns(createDishesRepo());
        }

        private void setupController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_DISH_NAME);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            testedController = new GeneralDishesController(mockUoW.Object);
            testedController.ControllerContext = controllerContext.Object;
        }

        [TestInitialize]
        public void initData()
        {
            setupMockUoW();
            setupController();
        }


        [TestMethod]
        public void TestDishesIndex()
        {
            // arrange.
            var controller = testedController;

            // act.
            var actionRes = controller.Index() as ViewResult;
            var expectedModel = mockUoW.Object.DishesRepository
                                    .GetAll()
                                    .Where(dish => dish.OwnerID == null)
                                    .ToList<Dish>();

            // assert.
            CollectionAssert.AreEquivalent(expectedModel as ICollection, actionRes.Model as ICollection, "");
        }

        [TestMethod]
        public void TestDishesCreate()
        {
            // arrange.
            var controller = testedController;

            // act.
            var actionRes = controller.Create(SAMPLE_RETURN_URL) as ViewResult;
            var model = actionRes.Model as DishViewModel;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, model.ReturnUrl);
            Assert.AreEqual(0, model.Dish.ID);
            Assert.AreEqual("Create", actionRes.ViewName);
        }

        [TestMethod]
        public void TestDishesGetEdit()
        {
            // arrange.
            var controller = testedController;
            Dish dishToEdit = mockUoW.Object.DishesRepository.GetAll().First();

            // act.
            var actionRes = controller.Edit(dishToEdit.ID, SAMPLE_RETURN_URL) as ViewResult;
            var model = actionRes.Model as DishViewModel;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, model.ReturnUrl);
            Assert.AreEqual(dishToEdit.ID, model.Dish.ID);
        }

        [TestMethod]
        public void TestDishesPostEdit()
        {
            // arrange.
            var controller = testedController;
            int dishToEditId = mockUoW.Object.DishesRepository.GetAll().First().ID;
            string newName = SAMPLE_DISH_NAME;

            Dish dishToEdit = mockUoW.Object.DishesRepository
                                    .GetAll()
                                    .Where(dish => dish.ID == dishToEditId)
                                    .First();

            DishViewModel dvm = new DishViewModel()
            {
                Dish = new Dish {
                    ID = dishToEdit.ID,
                    Name = newName,
                    Description = dishToEdit.Description,
                    OwnerID = dishToEdit.OwnerID
                },
                ReturnUrl = SAMPLE_RETURN_URL
            };


            // act.
            controller.Edit(dvm);

            // assert.
            mockDishesRepo.Verify(m => m.Update(It.IsAny<Dish>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestDishesDelete()
        {
            // arrange.
            var controller = testedController;
            int dishToDelId = mockUoW.Object.DishesRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.Delete(dishToDelId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, actionRes.Url);
            mockDishesRepo.Verify(m => m.Delete(It.IsAny<int>()));
        }

        [TestMethod]
        public void TestDishesAddToPersonal()
        {
            // arrange.
            var controller = testedController;
            int dishToAddId = mockUoW.Object.DishesRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.AddToPersonal(dishToAddId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(actionRes.Url, SAMPLE_RETURN_URL);
            mockDishesRepo.Verify(m => m.Insert(It.IsAny<Dish>()));
        }
    }
}
