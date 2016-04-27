using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.Abstract;
using OnlineDietManager.Domain.UsersManagement;

namespace OnlineDietManager.Domain.CoursesManagement
{
    [Table("Courses")]
    public class Course
        : IDescribable
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public virtual IList<Day> Days { get; set; }

        public string Description { get; set; }

        [StringLength(128)]
        public string OwnerID { get; set; }

        [ForeignKey("OwnerID")]
        public virtual AppUser Owner { get; set; }
    }
}
