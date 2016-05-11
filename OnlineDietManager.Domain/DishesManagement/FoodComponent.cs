using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using OnlineDietManager.Domain.Abstract;
using OnlineDietManager.Domain.UsersManagement;

namespace OnlineDietManager.Domain.DishesManagement
{
    public abstract class FoodComponent
        : IDescribable
    {
        [Required(ErrorMessage = "Please, specify a name")]
        [Column(Order = 1)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please, specify a description")]
        [Column(Order = 2)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(128)]
        [HiddenInput(DisplayValue = false)]
        public string OwnerID { get; set; }

        [ForeignKey("OwnerID")]
        public virtual AppUser Owner { get; set; }
    }
}
