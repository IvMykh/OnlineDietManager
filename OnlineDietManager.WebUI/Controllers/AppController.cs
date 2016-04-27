using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.WebUI.Models;

namespace OnlineDietManager.WebUI.Controllers
{
    public abstract class AppController : Controller
    {
        public AppUserPrincipal CurrentUser
        {
            get
            {
                return new AppUserPrincipal(this.User as ClaimsPrincipal);
            }
        }
    }
}