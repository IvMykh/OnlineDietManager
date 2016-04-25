using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineDietManager.Domain.Abstract;

namespace OnlineDietManager.Domain.CoursesManagement
{
    [Table("Days")]
    public class Day
        : IDescribable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Description { get; set; }

        [Required]
        public virtual IList<Meal> Meals { get; set; }
    }
}
