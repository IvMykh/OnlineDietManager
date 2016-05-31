using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Controllers;
using System.Security.Principal;
using System.Web.Mvc;
using OnlineDietManager.Domain.UsersManagement;

namespace OnlineDietManager.Tests.NutritionalSummary
{
    [TestClass]
    public class NutrSummaryTests
    {
        private const string SAMPLE_USER_ID = "sample id";
        private const string SAMPLE_USER_NAME = "sample user name";

        private Mock<IUnitOfWork>       mockUoW;
        private Mock<IRepository<Dish>> mockDishesRepo;
        private Mock<IRepository<Day>>  mockDaysRepo;

        private GeneralDishesController genDishesTestedController;
        private DaysController          daysTestedController;


        private List<Dish> getDishes()
        {
            return new List<Dish> {
                    new Dish { ID = 12,  Name = "Fruit Cake",   Description = "The potato",   OwnerID = null },
                    new Dish { ID = 13,  Name = "Carrot",       Description = "The carrot",   OwnerID = null },
                    new Dish { ID = 14,  Name = "Cucumber",     Description = "The cucumber", OwnerID = SAMPLE_USER_ID },
                };
        }
        private List<Day> getDays()
        {
            return new List<Day> {
                new Day { 
                    ID = 1, 
                    Meals = new List<Meal> { 
                        new Meal { Dishes = 
                            new List<Dish> { mockDishesRepo.Object.GetAll().First() } 
                        }
                    }
                }
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

            return mockDishesRepo.Object;
        }
        private IRepository<Day> createDaysRepo()
        {
            List<Day> days = getDays();

            mockDaysRepo = new Mock<IRepository<Day>>();
            mockDaysRepo.Setup(m => m.GetAll())
                .Returns(days.AsQueryable<Day>());
            mockDaysRepo
               .Setup(m => m.GetById(It.Is<int>(id => id == days.First().ID)))
               .Returns(days.First());

            return mockDaysRepo.Object;
        }

        private void setupMockUoW()
        {
            mockUoW = new Mock<IUnitOfWork>();

            mockUoW
                .SetupGet(m => m.DishesRepository)
                .Returns(createDishesRepo());
            mockUoW
                .SetupGet(m => m.DaysRepository)
                .Returns(createDaysRepo());
        }

        private void setupGenDishesController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_USER_NAME);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            genDishesTestedController = new GeneralDishesController(mockUoW.Object);
            genDishesTestedController.ControllerContext = controllerContext.Object;
        }
        private void setupDaysController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_USER_NAME);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            daysTestedController = new DaysController(mockUoW.Object);
            daysTestedController.ControllerContext = controllerContext.Object;
        }
        private void setupControllers()
        {
            setupGenDishesController();
            setupDaysController();
        }

        [TestInitialize]
        public void initData()
        {
            setupMockUoW();
            setupControllers();
        }



        [TestMethod]
        public void TestDishNutritionalSummary()
        {
            // arrange.
            GeneralDishesController testedController = genDishesTestedController;
            Dish testedDish = mockUoW.Object.DishesRepository.GetAll().First();
            
            var expectedRes = new OnlineDietManager.WebUI.Models.NutritionalSummary {
                Protein         = testedDish.Protein,
                Fat             = testedDish.Fat,
                Carbohydrates   = testedDish.Carbohydrates,
                Caloricity      = testedDish.Caloricity,
                Weight          = testedDish.Weight
            };

            // act.
            var actionRes = testedController.ViewNutritionalSummary(testedDish.ID) as PartialViewResult;
            var actualRes = actionRes.Model as OnlineDietManager.WebUI.Models.NutritionalSummary;

            // assert.
            Assert.AreEqual(expectedRes.Protein, actualRes.Protein);
            Assert.AreEqual(expectedRes.Fat, actualRes.Fat);
            Assert.AreEqual(expectedRes.Carbohydrates, actualRes.Carbohydrates);
            Assert.AreEqual(expectedRes.Caloricity, actualRes.Caloricity);
            Assert.AreEqual(expectedRes.Weight, actualRes.Weight);
        }

        [TestMethod]
        public void TestDayNutritionalSummary()
        {
            // arrange.
            DaysController testedController = daysTestedController;
            Day testedDay = mockUoW.Object.DaysRepository.GetAll().First();

            var expectedRes = new OnlineDietManager.WebUI.Models.NutritionalSummary {
                Protein         = testedDay.Meals.Sum(m => m.Protein),
                Fat             = testedDay.Meals.Sum(m => m.Fat),
                Carbohydrates   = testedDay.Meals.Sum(m => m.Carbohydrates),
                Caloricity      = testedDay.Meals.Sum(m => m.Caloricity),
                Weight          = testedDay.Meals.Sum(m => m.Weight)
            };

            // act.
            var actionRes = testedController.ViewNutritionalSummary(testedDay.ID) as PartialViewResult;
            var actualRes = actionRes.Model as OnlineDietManager.WebUI.Models.NutritionalSummary;

            // assert.
            Assert.AreEqual(expectedRes.Protein, actualRes.Protein);
            Assert.AreEqual(expectedRes.Fat, actualRes.Fat);
            Assert.AreEqual(expectedRes.Carbohydrates, actualRes.Carbohydrates);
            Assert.AreEqual(expectedRes.Caloricity, actualRes.Caloricity);
            Assert.AreEqual(expectedRes.Weight, actualRes.Weight);
        }
    }
}
