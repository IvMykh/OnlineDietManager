using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class AppUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string ReturnUrl { get; set; }
    }
}