using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.CoursesManagement;

namespace OnlineDietManager.WebUI.Models
{
    public class ListMealsForDayViewModel
    {
        public int DayId { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
        public string ReturnUrl { get; set; }
    }
}