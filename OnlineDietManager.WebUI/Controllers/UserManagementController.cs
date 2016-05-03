using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Infrastructure;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineDietManager.WebUI.Models;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.CoursesManagement;


namespace OnlineDietManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController 
        : Controller
    {
        private IUnitOfWork OdmUnitOfWork { get; set; }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        public UserManagementController(IUnitOfWork unitOfWork)
        {
            OdmUnitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            string userRoleName = AppRole.RoleTypeToString(AppRole.RoleType.User);
            string adminRoleName = AppRole.RoleTypeToString(AppRole.RoleType.Admin);

            AppRole adminRole = RoleManager.FindByName(adminRoleName);
            AppRole userRole = RoleManager.FindByName(userRoleName);

            IEnumerable<AppUserViewModel> users = UserManager.Users
                                            .Where(user => user.Roles.Select(r => r.RoleId).Contains(adminRole.Id))
                                            .Select(user => new AppUserViewModel {
                                                    UserId = user.Id,
                                                    UserName = user.UserName,
                                                    Role = adminRoleName
                                                })
                                            .Union(UserManager.Users
                                                .Where(user => user.Roles.Select(r => r.RoleId).Contains(userRole.Id))
                                                .Select(user => new AppUserViewModel {
                                                    UserId = user.Id,
                                                    UserName = user.UserName,
                                                    Role = userRoleName
                                                })
                                            ).OrderBy(userVM => userVM.Role)
                                             .ThenBy(userVM => userVM.UserName);
            return View(users);
        }

        [HttpPost]
        public ActionResult GrantAdminRights(string userId, string returnUrl)
        {
            AppUser userToGrantAdmin =
                UserManager.Users.Where(user => user.Id == userId)
                                 .First();

            if (userToGrantAdmin != null)
            {
                userToGrantAdmin.Roles.Clear();
                UserManager.AddToRole(userToGrantAdmin.Id, AppRole.RoleTypeToString(AppRole.RoleType.Admin));

                TempData["message"] = string.Format(
                    "User {0} has been successfully granted admin rights", userId);
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult DeleteUser(string userId, string returnUrl)
        {
            AppUser userToDelete = 
                UserManager.Users.Where(user => user.Id == userId)
                                 .First();
            
            if (userToDelete != null)
            {
                // Better way - cascading.
                deleteIngredients(userId);
                deleteDishes(userId);
                deleteCourses(userId);

                UserManager.Delete(userToDelete);

                TempData["message"] = string.Format(
                    "User {0} has been successfully deleted", userId);

            }

            return Redirect(returnUrl);
        }

        private void deleteIngredients(string userId)
        {
            IEnumerable<Ingredient> ingredients = OdmUnitOfWork.IngredientsRepository
                                                        .GetAll()
                                                        .Where(ing => ing.OwnerID == userId);
            foreach (var item in ingredients)
            {
                OdmUnitOfWork.IngredientsRepository.Delete(item);
            }

            OdmUnitOfWork.Save();
        }
        private void deleteDishes(string userId)
        {
            IEnumerable<Dish> dishes = OdmUnitOfWork.DishesRepository
                                                        .GetAll()
                                                        .Where(dish => dish.OwnerID == userId);
            foreach (var item in dishes)
            {
                OdmUnitOfWork.DishesRepository.Delete(item);
            }

            OdmUnitOfWork.Save();
        }
        private void deleteCourses(string userId)
        {
            IEnumerable<Course> courses = OdmUnitOfWork.CoursesRepository
                                                        .GetAll()
                                                        .Where(course => course.OwnerID == userId);
            foreach (var item in courses)
            {
                OdmUnitOfWork.CoursesRepository.Delete(item);
            }

            OdmUnitOfWork.Save();
        }
	}
}