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
    public class GeneralIngredientsControllerTests
    {
        private const string SAMPLE_USER_ID = "sample id";
        private const string SAMPLE_USER_NAME = "sample name";
        private const string SAMPLE_RETURN_URL = "return url";

        private Mock<IUnitOfWork>               mockUoW;
        private Mock<IRepository<Ingredient>>   mockIngredientsRepo;
        private GeneralIngredientsController    testedController;
        
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

        private void setupMockUoW()
        {
            mockUoW = new Mock<IUnitOfWork>();
            
            mockUoW
                .SetupGet(m => m.IngredientsRepository)
                .Returns(createIngredientsRepo());
        }

        private void setupController()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(AppRole.RoleTypeToString(AppRole.RoleType.Admin))).Returns(true);
            principal.SetupGet(p => p.Identity.Name).Returns(SAMPLE_USER_NAME);
            
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(cc => cc.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(cc => cc.HttpContext.Request.IsAuthenticated).Returns(true);

            testedController = new GeneralIngredientsController(mockUoW.Object);
            testedController.ControllerContext = controllerContext.Object;
        }

        [TestInitialize]
        public void initData()
        {
            setupMockUoW();
            setupController();
        }


        [TestMethod]
        public void TestIngredientsIndex()
        {
            // arrange.
            var controller = testedController;
 
            // act.
            var actionRes = controller.Index() as ViewResult;
            var expectedModel = mockUoW.Object.IngredientsRepository
                                    .GetAll()
                                    .Where(ing => ing.OwnerID == null)
                                    .ToList<Ingredient>();

            // assert.
            CollectionAssert.AreEquivalent(expectedModel as ICollection, actionRes.Model as ICollection, "");
        }

        [TestMethod]
        public void TestIngredientsCreate()
        {
            // arrange.
            var controller = testedController;

            // act.
            var actionRes = controller.Create(SAMPLE_RETURN_URL) as ViewResult;
            var model = actionRes.Model as IngredientViewModel;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, model.ReturnUrl);
            Assert.AreEqual(0, model.Ingredient.ID);
            Assert.AreEqual("Edit", actionRes.ViewName);
        }

        [TestMethod]
        public void TestIngredientsGetEdit()
        {
            // arrange.
            var controller = testedController;
            Ingredient ingToEdit = mockUoW.Object.IngredientsRepository.GetAll().First();

            // act.
            var actionRes = controller.Edit(ingToEdit.ID, SAMPLE_RETURN_URL) as ViewResult;
            var model = actionRes.Model as IngredientViewModel;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, model.ReturnUrl);
            Assert.AreEqual(ingToEdit.ID, model.Ingredient.ID);
        }

        [TestMethod]
        public void TestIngredientsPostEdit()
        {
            // arrange.
            var controller = testedController;
            int ingToEditId = mockUoW.Object.IngredientsRepository.GetAll().First().ID;
            int newProtein = 500;

            Ingredient ingToEdit = mockUoW.Object.IngredientsRepository
                                    .GetAll()
                                    .Where(ing => ing.ID == ingToEditId)
                                    .First();
            
            IngredientViewModel ivm = new IngredientViewModel() { 
                Ingredient = new Ingredient { 
                    ID              = ingToEdit.ID,
                    Name            = ingToEdit.Name,
                    Description     = ingToEdit.Description,
                    Protein         = newProtein,
                    Fat             = ingToEdit.Fat,
                    Carbohydrates   = ingToEdit.Carbohydrates,
                    Caloricity      = ingToEdit.Caloricity,
                    OwnerID         = ingToEdit.OwnerID
                },
                ReturnUrl = SAMPLE_RETURN_URL 
            };


            // act.
            controller.Edit(ivm);

            // assert.
            mockIngredientsRepo.Verify(m => m.Update(It.IsAny<Ingredient>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestIngredientsDelete()
        {
            // arrange.
            var controller = testedController;
            int ingToDelId = mockUoW.Object.IngredientsRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.Delete(ingToDelId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(SAMPLE_RETURN_URL, actionRes.Url);
            mockIngredientsRepo.Verify(m => m.Delete(It.IsAny<int>()));
        }

        [TestMethod]
        public void TestIngredientsAddToPersonal()
        {
            // arrange.
            var controller = testedController;
            int ingToAddId = mockUoW.Object.IngredientsRepository.GetAll().First().ID;

            // act.
            var actionRes = controller.AddToPersonal(ingToAddId, SAMPLE_RETURN_URL) as RedirectResult;

            // assert.
            Assert.AreEqual(actionRes.Url, SAMPLE_RETURN_URL);
            mockIngredientsRepo.Verify(m => m.Insert(It.IsAny<Ingredient>()));
        }
    }
}
