using OnlineDietManager.Domain.CoursesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class ListDayViewModel
    {
        public IEnumerable<Day> Days { get; set; }
        public int CourseRefId { get; set; }
        public string ReturnUrl { get; set; }
    }
}