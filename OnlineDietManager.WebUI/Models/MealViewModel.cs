using OnlineDietManager.Domain.CoursesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class MealViewModel
    {
        public Meal Meal { get; set; }
        public string returnUrl { get; set; }
        public int Day_ID { get; set; }
    }
}