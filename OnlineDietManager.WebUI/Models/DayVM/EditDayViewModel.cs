using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.CoursesManagement;

namespace OnlineDietManager.WebUI.Models
{
    public class EditDayViewModel
    {
        public Day Day { get; set; }
        public string ReturnUrl { get; set; }
    }
}