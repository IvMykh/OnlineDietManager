using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Controllers;
using System.Web.Mvc;
using OnlineDietManager.Domain.UsersManagement;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace OnlineDietManager.Tests
{
    public class MyIdentity
            : IIdentity
    {
        public string Id { get; set; }

        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
    }

    //public static class MyIdentityExtensions
    //{
    //    public static string GetUserId(this IIdentity myIdentity)
    //    {
    //        return myIdentity.GetUserId();
    //    }
    //}

    [TestClass]
    public class IngredientsControllerTests
    {
        private static Mock<IUnitOfWork> mockUoW;
        private static string            testedUser = "MY_USER_ID";

        private static List<Ingredient> getIngredients()
        {
            return new List<Ingredient> {
                    new Ingredient { ID = 6,  Name = "Potato",   Description = "The potato",   Protein = 5.0f,  Fat = 0.4f,  Carbohydrates = 16.3f, Caloricity = 72.7f },
                    new Ingredient { ID = 7,  Name = "Carrot",   Description = "The carrot",   Protein = 1.3f,  Fat = 0.1f,  Carbohydrates = 6.9f,  Caloricity = 32.0f },
                    new Ingredient { ID = 8,  Name = "Cucumber", Description = "The cucumber", Protein = 0.8f,  Fat = 0.1f,  Carbohydrates = 3.0f,  Caloricity = 15.4f },
                    new Ingredient { ID = 9,  Name = "Apple",    Description = "The apple",    Protein = 0.4f,  Fat = 0.4f,  Carbohydrates = 9.8f,  Caloricity = 44.0f },
                    new Ingredient { ID = 10, Name = "Egg",      Description = "The egg",      Protein = 12.7f, Fat = 11.5f, Carbohydrates = 0.7f,  Caloricity = 157.0f },
                };
        }

        private static IRepository<Ingredient> createIngredientsRepo()
        {
            List<Ingredient> ingredients = getIngredients();

            var mockRepo = new Mock<IRepository<Ingredient>>();
            mockRepo.Setup(m => m.GetAll())
                .Returns(ingredients.AsQueryable<Ingredient>());

            return mockRepo.Object;
        }

        [ClassInitialize]
        public static void initmockUoW(TestContext context)
        {
            mockUoW = new Mock<IUnitOfWork>();
            mockUoW
                .Setup(m => m.IngredientsRepository)
                .Returns(createIngredientsRepo());
        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    //var validPrincipal = new ClaimsPrincipal(new[] {
        //    //    new ClaimsIdentity(new[] {
        //    //        new Claim(ClaimTypes.NameIdentifier, "MyUserId")
        //    //    })
        //    //});
        //
        //    var mock = new Mock<ControllerContext>();
        //    mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("1");
        //    mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
        //
        //    var controller = new IngredientsController(mockUoW.Object);
        //    controller.ControllerContext = mock.Object;
        //
        //    // arrange.
        //    Assert.AreEqual(controller.User.Identity.IsAuthenticated, true);
        //    //// act.
        //    //var actionRes = ingController.Index() as ViewResult;
        //    //
        //    //// assert.
        //    //Assert.AreEqual(actionRes.Model, mockUoW.Object.IngredientsRepository.GetAll());
        //}

        //[TestMethod]
        //public void Test2()
        //{
        //    var identity = new GenericIdentity("tugberk");
        //    var controller = new IngredientsController(mockUoW.Object);

        //    var principal = new Mock<IPrincipal>();
        //    principal.Setup(p => p.IsInRole("Admin")).Returns(true);
        //    principal.SetupGet(x => x.Identity.Name).Returns("tugberk");
        //    //principal.Setup(x => x.Identity.GetUserId()).Returns("1");

        //    var controllerContext = new Mock<ControllerContext>();
        //    controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
        //    controller.ControllerContext = controllerContext.Object;

        //    Assert.AreEqual(controller.User.Identity.Name, identity.Name);
        //    Assert.IsNotNull((controller.Index() as ViewResult).Model);
        //    Assert.IsNotNull(controller.User.Identity.GetUserId());
        //}

        //[TestMethod]
        //public void Test3()
        //{
        //    var identity = new GenericIdentity("tugberk");
        //    var controller = new IngredientsController(mockUoW.Object);
        //
        //    var principal = new Mock<IPrincipal>();
        //
        //    principal.SetupGet(p => p.Identity).Returns(new IIdentity(). { Id = "1", IsAuthenticated = true, Name = "Ivan" });
        //
        //    principal.Setup(p => p.IsInRole("Admin")).Returns(true);
        //    //principal.SetupGet(x => x.Identity.Name).Returns("tugberk");
        //    //principal.Setup(x => x.Identity.GetUserId()).Returns("1");
        //
        //    var controllerContext = new Mock<ControllerContext>();
        //    controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
        //    controller.ControllerContext = controllerContext.Object;
        //
        //    //Assert.AreEqual(controller.User.Identity.Name, identity.Name);
        //    //Assert.IsNotNull((controller.Index() as ViewResult).Model);
        //    Assert.IsNotNull(controller.User.Identity.GetUserId());
        //}
    }
}
