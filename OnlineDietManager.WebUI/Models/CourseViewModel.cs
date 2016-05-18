using OnlineDietManager.Domain.CoursesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public int index { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class CourseViewWithDayIdModel
    {
        public CourseViewModel CVM { get; set; }
        public int index { get; set; }
    }
}