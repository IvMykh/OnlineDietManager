﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineDietManager.Domain.Entities.UsersManagement
{
    [Table("Users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        //[Index(IsUnique=true)]
        [StringLength(50)]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
