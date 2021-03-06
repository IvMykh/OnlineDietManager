﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OnlineDietManager.Domain;
using OnlineDietManager.Domain.UsersManagement;
using OnlineDietManager.WebUI.Infrastructure;
using OnlineDietManager.WebUI.Models;
using Owin;

namespace OnlineDietManager.WebUI
{
    public class Startup
    {
        public static Func<UserManager<AppUser>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });

            // configure the user manager
            // ...
            // taken from Roman's link; now replaced.

            // Roles management config.
            app.CreatePerOwinContext<OnlineDietManagerContext>(OnlineDietManagerContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
        }
    }
}