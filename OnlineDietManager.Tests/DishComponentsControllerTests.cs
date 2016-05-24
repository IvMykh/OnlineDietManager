using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Controllers;
using System.Security.Principal;
using OnlineDietManager.Domain.UsersManagement;
using System.Web.Mvc;
using System.Collections;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.Tests
{
    [TestClass]
    public class DishComponentsControllerTests
    {
        private const string SAMPLE_USER_ID = "sample id";
        private const string SAMPLE_USER_NAME = "sample user name";
        private const string SAMPLE_DISH_NAME = "sample dish name";
        private const string SAMPLE_RETURN_URL = "return url";

        private Mock<IUnitOfWork>                   mockUoW;
        private Mock<IRepository<Ingredient>>       mockIngredientsRepo;
        private Mock<IRepository<Dish>>             mockDishesRepo;
        private Mock<IRepository<DishComponent>>    mockDishCompsRepo;

        private DishComponentsController            testedController;

        private List<Ingredient> getIngredients()
        {
            return new List<Ingredient> {
                    new Ingredient { ID = 6,  Name = "Potato",   Description = "The potato",   Protein = 5.0f,  Fat = 0.4f,  Carbohydrates = 16.3f, Caloricity = 72.7f,  OwnerID = null },
                    new Ingredient { ID = 7,  Name = "Carrot",   Description = "The carrot",   Protein = 1.3f,  Fat = 0.1f,  Carbohydrates = 6.9f,  Caloricity = 32.0f,  OwnerID = null },
                    new Ingredient { ID = 8,  Name = "Cucumber", Description = "The cucumber", Protein = 0.8f,  Fat = 0.1f,  Carbohydrates = 3.0f,  Caloricity = 15.4f,  OwnerID = SAMPLE_USER_ID },
                    new Ingredient { ID = 9,  Name = "Apple",    Description = "The apple",    Protein = 0.4f,  Fat = 0.4f,  Carbohydrates = 9.8f,  Caloricity = 44.0f,  OwnerID = null },
                    new Ingredient { ID = 10, Name = "Egg",      Description = "The egg",      Protein = 12.7f, Fat = 11.5f, Carbohydrates = 0.7f,  Caloricity = 157.0f, OwnerID = SAMPLE_USER_ID },
                };
        }
        private List<Dish> getDishes()
        {
            return new List<Dish> {
                    new Dish { ID = 12,  Name = "Fruit Cake",   Description = "The potato",   OwnerID = null },
                    new Dish { ID = 13,  Name = "Carrot",       Description = "The carrot",   OwnerID = null },
                    new Dish { ID = 14,  Name = "Cucumber",     Description = "The cucumber", OwnerID = SAMPLE_USER_ID },
                };
        }
        private List<DishComponent> getDishComponent()
        {
            return new List<DishComponent> {
                    new DishComponent   { ID = 6, Ingredient = mockUoW.Object.IngredientsRepository.GetById(6), DishRefID = 12, Weight = 100 }
                };
        }

        private IRepository<Ingredient> createIngredientsRepo()
        {
            List<Ingredient> ingredients = getIngredients();

            mockIngredientsRepo = new Mock<IRepository<Ingredient>>();
            mockIngredientsRepo.Setup(m => m.GetAll())
                .Returns(ingredients.AsQueryable<Ingredient>());

            mockIngredientsRepo
                .Setup(m => m.GetById(It.Is<int>(id => id == ingredients.First().ID)))
                .Returns(ingredients.First());

            mockIngredientsRepo.Setup(m => m.Update(It.IsAny<Ingredient>()))
                .Verifiable();

            mockIngredientsRepo.Setup(m => m.Delete(It.IsAny<int>()))
                .Verifiable();

            mockIngredientsRepo.Setup(m => m.Insert(It.IsAny<Ingredient>()))
                .Verifiable();


            return mockIngredientsRepo.Object;
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
        private IRepository<DishComponent> createDishComponentsRepo()
        {
            List<DishComponent> dishComponents = getDishComponent();

            mockDishCompsRepo = new Mock<IRepository<DishComponent>>();
            mockDishCompsRepo.Setup(m => m.GetAll())
                .Returns(dishComponents.AsQueryable<DishComponent>());

            mockDishCompsRepo
                .Setup(m => m.GetById(It.Is<int>(id => id == dishComponents.First().ID)))
                .Returns(dishComponents.First());

            mockDishCompsRepo.Setup(m => m.Update(It.IsAny<DishComponent>()))
                .Verifiable();

            mockDishCompsRepo.Setup(m => m.Delete(It.IsAny<int>()))
                .Verifiable();

            mockDishCompsRepo.Setup(m => m.Insert(It.IsAny<DishComponent>()))
                .Verifiable();


            return mockDishCompsRepo.Object;
        }

        private void setupMockUoW()
        {
            mockUoW = new Mock<IUnitOfWork>();

            mockUoW
                .SetupGet(m => m.IngredientsRepository)
                .Returns(createIngredientsRepo());

            mockUoW
                .SetupGet(m => m.DishesRepository)
                .Returns(createDishesRepo());

            mockUoW
                .SetupGet(m => m.DishComponentsRepository)
                .Returns(createDishComponentsRepo());
        }

        private void setupController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_DISH_NAME);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            testedController = new DishComponentsController(mockUoW.Object);
            testedController.ControllerContext = controllerContext.Object;
        }

        [TestInitialize]
        public void initData()
        {
            setupMockUoW();
            setupController();
        }



        [TestMethod]
        public void TestDishCompnentsIndex()
        {
            // arrange.
            var controller = testedController;
            int dishId = mockUoW.Object.DishesRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.Index(dishId, SAMPLE_RETURN_URL) as PartialViewResult;
            var expectedModel = mockUoW.Object.DishComponentsRepository
                                    .GetAll()
                                    .Where(dishComp => dishComp.DishRefID == dishId)
                                    .ToList<DishComponent>();

            // assert.
            CollectionAssert.AreEquivalent(expectedModel as ICollection, (actionRes.Model as ListDishComponentsViewModel).DishComponents as ICollection, "");
        }

        [TestMethod]
        public void TestDishCompnentsGetCreate()
        {
            // arrange.
            var controller = testedController;
            int dishId = mockUoW.Object.DishesRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.Create(SAMPLE_RETURN_URL, dishId, OwnerPolicy.GlobalOnly) as PartialViewResult;

            var model = (actionRes.Model as IEnumerable<SelectIngredientViewModel>);
            IEnumerable<SelectIngredientViewModel> expectedModel = mockUoW.Object.IngredientsRepository
                                .GetAll()
                                .Where(ing => ing.OwnerID == null)
                                .Select(ing => new SelectIngredientViewModel {
                                    Ingredient = ing,
                                    DishRefId = dishId,
                                    Weight = SpecialData.DEFAULT_COMPONENT_WEIGHT,
                                    ReturnUrl = SAMPLE_RETURN_URL
                                });
            // assert.
            CollectionAssert.AreEquivalent(expectedModel as ICollection, model as ICollection, "");
        }
    }
}
