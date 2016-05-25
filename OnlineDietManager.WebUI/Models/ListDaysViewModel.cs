using OnlineDietManager.Domain.CoursesManagement;
using System;
using System.Collections.Generic;

namespace OnlineDietManager.WebUI.Models
{
    public class ListDaysViewModel
    {
        public IEnumerable<Day> Days { get; set; }
        public int CourseRefId { get; set; }
        public string ReturnUrl { get; set; }
    }
}