using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDietManager.Domain.DishesManagement;

namespace OnlineDietManager.WebUI.Models
{
    public class DishComponentViewModel
    {
        public DishComponent DishComponent { get; set; }
        public string ReturnUrl { get; set; }
    }
}