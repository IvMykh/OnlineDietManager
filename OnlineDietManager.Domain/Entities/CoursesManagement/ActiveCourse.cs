using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.CoursesManagement
{
    public class ActiveCourse
        : Course
    {
        public DateTime StartDate { get; set; }
    }
}
