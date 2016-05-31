using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.DishesManagement;

namespace OnlineDietManager.WebUI.Models
{
    public enum OwnerPolicy
    {
        UserOnly,
        GlobalOnly,
        Both
    }

    public class ListDishComponentsViewModel
    {
        public IEnumerable<DishComponent> DishComponents { get; set; }
        public int DishRefId { get; set; }
        public string ReturnUrl { get; set; }

        public OwnerPolicy OwnerPolicy { get; set; }
    }
}